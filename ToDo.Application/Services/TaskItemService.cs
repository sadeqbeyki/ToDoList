using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Application.DTOs.TaskItem;
using ToDo.Application.DTOs.TaskItems;
using System.Linq;
using ToDo.Application.Interfaces;
using AutoMapper;
using ToDo.Domain.Models;

namespace ToDo.Application.Services;
public class TaskItemService(ITaskRepository taskRepository, IMapper mapper) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<TaskItemDto?> GetByIdAsync(long id)
    {
        var task = await _taskRepository.GetTaskItemWithTaskList(id);
        if (task == null) return null;

        return new TaskItemDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsDone = task.IsDone,
            TaskListId = task.TaskListId,
            TaskListTitle = task.TaskList?.Name ?? ""
        };
    }
    public async Task<List<TaskItemDto>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllTaskItem();

        return tasks.Select(x => new TaskItemDto
        {
            Id = x.Id,
            Title = x.Title,
            CreationDate = x.CreationDate.ToFarsi(),
            Description = x.Description,
            IsDone = x.IsDone,
            TaskListTitle = x.TaskList?.Name ?? ""
        }).ToList();
    }

    public async Task<OperationResult> Create(CreateTaskDto command)
    {
        var operation = new OperationResult();
        if (await _taskRepository.Exists(x => x.Title == command.Title))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var task = new TaskItem(command.Title, command.Description, command.TaskListId);
        await _taskRepository.Create(task);
        await _taskRepository.SaveChangesAsync();
        return operation.Succeeded();
    }
    public async Task<OperationResult> Edit(EditTaskDto command)
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


    public async Task<List<TaskItemDto>> SearchAsync(SearchTaskItemDto filter)
    {
        var searchModel = new TaskItemSearchModel
        {
            Title = filter.Title,
            IsDone = filter.IsDone,
            TaskListId = filter.TaskListId
        };

        var query = await _taskRepository.SearchAsync(searchModel);

        return [.. query.Select(x => new TaskItemDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            IsDone = x.IsDone,
            TaskListTitle = x.TaskList?.Name,
            TaskListId = x.TaskListId,
            CreationDate = x.CreationDate.ToFarsi(),
        }).OrderByDescending(x => x.CreationDate)];

    }


    public async Task DeleteAsync(long id)
    {
        await _taskRepository.Delete(id);
        await _taskRepository.SaveChangesAsync();
    }

    public async Task ToggleIsDone(long id, bool isDone)
    {
        var task = await _taskRepository.GetAsync(id) ?? throw new Exception("Task not found");
        task.IsDone = isDone;
        await _taskRepository.SaveChangesAsync();
    }

}
