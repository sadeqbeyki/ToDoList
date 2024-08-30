using AppFramework.Application;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;
using ToDo.Application.Interfaces;
using ToDo.Domain.DTOs;
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

    public async Task<List<TaskListViewModel>> GetAllTaskList()
    {
        var list = await _taskCategoryRepository.GetAllTaskLists();
        return _mapper.Map<List<TaskListViewModel>>(list);
    }

    public async Task<TaskListViewModel> GetDetails(long id)
    {
        var entity = await _taskCategoryRepository.GetAsync(id);
        return _mapper.Map<TaskListViewModel>(entity);
    }


    public async Task<List<TaskListViewModel>> SearchAsync(TaskListSearchModel filter)
    {
        var searchModel =  _mapper.Map<TaskListSearchDto>(filter);
        var result = await _taskCategoryRepository.SearchAsync(searchModel);
        var mappedResult =  _mapper.Map<List<TaskListViewModel>>(result);

        return mappedResult;
    }
    public async Task DeleteAsync(long id)
    {
        _taskCategoryRepository.Delete(id);
        await _taskCategoryRepository.SaveChangesAsync();
    }
}
