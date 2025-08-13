using AppFramework.Application;
using AppFramework.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItems;
using ToDo.Application.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.Services;
public class TaskItemService(ITaskRepository taskRepository,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor,
    ICurrentUserService currentUserService) : ITaskService
{
    private readonly ITaskRepository _taskRepository = taskRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    public async Task<TaskItemViewModel?> GetByIdAsync(long id)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var task = await _taskRepository.GetTaskItemWithListName(id);
        if (task == null) return null;

        return new TaskItemViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsDone = task.IsDone,
            TaskListId = task.TaskListId,
            TaskListTitle = task.TaskList?.Name ?? ""
        };
    }
    public async Task<List<TaskItemViewModel>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllTaskItem();

        return tasks.Select(x => new TaskItemViewModel
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

        if (!_currentUserService.IsAuthenticated)
            return operation.Failed("User is probably not authenticated");

        if (await _taskRepository.Exists(x => x.Title == command.Title))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        var task = new TaskItem(command.Title, command.Description, command.TaskListId, _currentUserService.UserId!);
        await _taskRepository.Create(task);
        await _taskRepository.SaveChangesAsync();
        return operation.Succeeded();
    }
    public async Task<OperationResult> Edit(EditTaskDto command)
    {
        var operation = new OperationResult();
        var task = await _taskRepository.GetTaskItemWithListName(command.Id);
        if (task == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        if (await _taskRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);

        task.Edit(command.Title, command.Description, command.TaskListId, command.IsDone);

        await _taskRepository.SaveChangesAsync();
        return operation.Succeeded();
    }

    public async Task<List<TaskItemViewModel>> Search(TaskItemSearchModel filter)
    {
        var searchModel = _mapper.Map<TaskItemSearchDto>(filter);
        var result = await _taskRepository.Search(searchModel);
        var mappedResult = _mapper.Map<List<TaskItemViewModel>>(result);

        return mappedResult;
    }

    public async Task<OperationResult> Delete(long id)
    {
        var result = new OperationResult();

        var taskList = await _taskRepository.GetTaskItemById(id);
        if (taskList == null)
            return result.Failed("Task list not found.");

        _taskRepository.Delete(taskList);
        await _taskRepository.SaveChangesAsync();

        return result.Succeeded();
    }

    public async Task ToggleIsDone(long id, bool isDone)
    {
        var task = await _taskRepository.GetAsync(id) ?? throw new Exception("Task not found");
        task.IsDone = isDone;
        await _taskRepository.SaveChangesAsync();
    }

    public async Task<PaginatedList<TaskItemViewModel>> GetPaginated(int pageIndex, int pageSize)
    {
        var pagedTasks = await _taskRepository.GetAllPaginatedAsync(pageIndex, pageSize);

        return new PaginatedList<TaskItemViewModel>(
            [.. pagedTasks.Select(t => new TaskItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsDone = t.IsDone,
                TaskListTitle = t.TaskList.Name,
                TaskListId = t.TaskListId,
                CreationDate = t.CreationDate.ToFarsi()
            })],
            pagedTasks.TotalItems,
            pagedTasks.PageIndex,
            pageSize
        );
    }

    public async Task<PaginatedList<TaskItemViewModel>> SearchPaginated(TaskItemSearchModel searchModel, int pageIndex, int pageSize)
    {
        var query = _taskRepository.GetQueryable();
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
            throw new Exception("❌ userId is null. User is probably not authenticated.");


        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            query = query.Where(x => x.Title.Contains(searchModel.Title));

        if (searchModel.IsDone.HasValue)
            query = query.Where(x => x.IsDone == searchModel.IsDone.Value);

        if (searchModel.TaskListId > 0)
            query = query.Where(x => x.TaskListId == searchModel.TaskListId);

        var totalCount = await query.CountAsync();

        var tasks = await query.Where(t => t.UserId == userId)
            .OrderByDescending(x => x.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TaskItemViewModel
            {
                Id = x.Id,
                Title = x.Title,
                IsDone = x.IsDone,
                TaskListId = x.TaskListId,
                TaskListTitle = x.TaskList.Name,
                CreationDate = x.CreationDate.ToString("yyyy-MM-dd")
            })
            .ToListAsync();

        return new PaginatedList<TaskItemViewModel>(tasks, totalCount, pageIndex, pageSize);
    }

}
