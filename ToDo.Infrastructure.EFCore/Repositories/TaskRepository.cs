using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskRepository : RepositoryBase<long, TaskItem>, ITaskRepository
{
    private readonly ToDoContext _taskContext;

    public TaskRepository(ToDoContext taskContext) : base(taskContext)
    {
        _taskContext = taskContext;
    }

    public async Task<List<TaskViewModel>> GetAllTaskItem()
    {
        return await _taskContext.TaskItems.Select(x => new TaskViewModel
        {
            Id = x.Id,
            Title = x.Title,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToListAsync();
    }

    public async Task<TaskItem> GetTaskItemWithTaskList(long id)
    {
        return await _taskContext.TaskItems.Include(x => x.TaskList).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<EditTask> GetTaskItemById(long id)
    {
        return await _taskContext.TaskItems.Select(x => new EditTask
        {
            Id = x.Id,
            Title = x.Title,
            IsDone = x.IsDone,
            TaskListId = x.TaskListId,
            Description = x.Description
        }).FirstOrDefaultAsync(x => x.Id == id);
    }

    public List<TaskViewModel> Search(TaskSearchModel searchModel)
    {
        var query = _taskContext.TaskItems.Include(x => x.TaskList).Select(x => new TaskViewModel
        {
            Id = x.Id,
            Title = x.Title,
            IsDone = x.IsDone,
            TaskListId = x.TaskListId,
            TaskList = x.TaskList.Name,
            CreationDate = x.CreationDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Title.Contains(searchModel.Name));

        if (searchModel.TaskListId != 0)
            query = query.Where(x => x.TaskListId == searchModel.TaskListId);

        return query.OrderByDescending(x => x.Id).ToList();
    }

}
