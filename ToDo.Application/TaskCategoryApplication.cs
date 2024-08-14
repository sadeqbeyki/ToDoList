using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskList;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Application;

public class TaskCategoryApplication(ITaskCategoryRepository taskCategoryRepository) : ITaskCategoryApplication
{
    private readonly ITaskCategoryRepository _taskCategoryRepository = taskCategoryRepository;

    public async Task<OperationResult> Create(CreateTaskCategory command)
    {
        var operation = new OperationResult();
        if (await _taskCategoryRepository.Exists(x => x.Name == command.Name))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var taskCategory = new TaskList(command.Name, command.Description);
        await _taskCategoryRepository.Create(taskCategory);
        await _taskCategoryRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<OperationResult> Edit(EditTaskCategory command)
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

    public async Task<List<TaskCategoryViewModel>> GetTaskList()
    {
        return await _taskCategoryRepository.GetTaskCategories();
    }

    public async Task<EditTaskCategory> GetDetails(long id)
    {
        return await _taskCategoryRepository.GetDetails(id);
    }


    public List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel)
    {
        return _taskCategoryRepository.Search(searchModel);
    }
}
