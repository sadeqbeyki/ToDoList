using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ToDo.Domain.Interfaces;
using ToDo.Application.Contracts.TaskItem;
using ToDo.Domain.Entities;

namespace ToDo.Application;
public class TaskApplication(ITaskRepository taskRepository) : ITaskApplication
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<OperationResult> Create(CreateTask command)
    {
        var operation = new OperationResult();
        if (await _taskRepository.Exists(x => x.Title == command.Title))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var task = new TaskItem(command.Title, command.Description, command.TaskListId);
        await _taskRepository.Create(task);
        await _taskRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<OperationResult> Edit(EditTask command)
    {
        var operation = new OperationResult();
        var task = await _taskRepository.GetTaskItemWithTaskList(command.Id);
        if (task == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (await _taskRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        task.Edit(command.Title, command.Description, command.TaskListId, command.IsDone);

        await _taskRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<List<TaskViewModel>> GetTasks()
    {
        return await _taskRepository.GetAllTaskItem();
    }

    public async Task<EditTask> GetDetails(long id)
    {
        return await _taskRepository.GetTaskItemById(id);
    }

    public List<TaskViewModel> Search(TaskSearchModel searchModel)
    {
        return _taskRepository.Search(searchModel);
    }

    public async Task ToggleIsDone(long id, bool isDone)
    {
        var task = await _taskRepository.GetAsync(id) ?? throw new Exception("Task not found");
        task.IsDone = isDone;
        await _taskRepository.SaveChangesAsync();
    }

}
