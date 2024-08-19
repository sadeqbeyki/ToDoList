using AppFramework.Application;
using AppFramework.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItems;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Application.Services;

public class TaskListService(ITaskListRepository taskCategoryRepository, IMapper mapper) : ITaskListService
{
    private readonly ITaskListRepository _taskCategoryRepository = taskCategoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<OperationResult> Create(CreateTaskListDto command)
    {
        var operation = new OperationResult();
        if (await _taskCategoryRepository.Exists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var taskCategory = new TaskList(command.Name, command.Description);
        await _taskCategoryRepository.Create(taskCategory);
        await _taskCategoryRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<OperationResult> Edit(EditTaskListDto command)
    {
        var operation = new OperationResult();
        var taskCategory = await _taskCategoryRepository.GetAsync(command.Id);
        if (taskCategory == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (await _taskCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        taskCategory.Edit(command.Name, command.Description);
        await _taskCategoryRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<List<TaskListDto>> GetAllTaskList()
    {
        var list = await _taskCategoryRepository.GetAllTaskLists();
        return _mapper.Map<List<TaskListDto>>(list);
    }

    public async Task<TaskListDto> GetDetails(long id)
    {
        var entity = await _taskCategoryRepository.GetAsync(id);
        return _mapper.Map<TaskListDto>(entity);
    }


    public async Task<List<TaskListDto>> Search(SearchTaskListDto filter)
    {
        var searchModel =  _mapper.Map<TaskList>(filter);

        var result = _taskCategoryRepository.Search(searchModel);
        return  _mapper.Map<List<TaskListDto>>(result);
    }
}
