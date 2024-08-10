using AppFramework.Application;
using ToDo.Application.Contracts.Task;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFramework.Domain;
using System;
using ToDo.Domain.Interfaces;

namespace ToDo.Application;
public class TaskApplication(ITaskRepository taskRepository) : ITaskApplication
{
    private readonly ITaskRepository _taskRepository = taskRepository;

    public OperationResult Create(CreateTask command)
    {
        var operation = new OperationResult();
        if (_taskRepository.Exists(x => x.Title == command.Title))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var task = new TaskItem(command.Title, command.Description, command.TaskListId);
        _taskRepository.Create(task);
        _taskRepository.SaveChanges();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditTask command)
    {
        var operation = new OperationResult();
        var task = _taskRepository.GetTaskItemWithTaskList(command.Id);
        if (task == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (_taskRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        task.Edit(command.Title, command.Description, command.TaskListId, command.IsDone);

        _taskRepository.SaveChanges();
        return operation.Succeeded();
    }

    public List<TaskViewModel> GetTasks()
    {
        return _taskRepository.GetAllTaskItem();
    }

    public EditTask GetDetails(long id)
    {
        return _taskRepository.GetTaskItemById(id);
    }

    public List<TaskViewModel> Search(TaskSearchModel searchModel)
    {
        return _taskRepository.Search(searchModel);
    }

    public async Task<TaskViewModel> GetTask(long id)
    {
        return await _taskRepository.GetByIdAsync(id);

    }

    public async Task ToggleIsDone(long id, bool isDone)
    {
        var task = await _taskRepository.GetAsync(id);
        if (task == null) throw new Exception("Task not found");

        task.IsDone = isDone;
        await _taskRepository.SaveChangesAsync();
    }

}
