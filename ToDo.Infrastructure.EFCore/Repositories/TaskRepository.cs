using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskRepository : RepositoryBase<long, TaskItem>, ITaskRepository
{
    private readonly ToDoContext _taskContext;

    public TaskRepository(ToDoContext taskContext) : base(taskContext)
    {
        _taskContext = taskContext;
    }


    public async Task<List<TaskItem>> GetAllTaskItem()
        => await _taskContext.TaskItems.ToListAsync();

    public async Task<TaskItem> GetTaskItemById(long id)
        => await _taskContext.TaskItems
                 .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<TaskItem> GetTaskItemWithTaskList(long id)
    => await _taskContext.TaskItems
                     .Include(t => t.TaskList)
                     .FirstOrDefaultAsync(t => t.Id == id);


    public async Task<List<TaskItem>> SearchAsync(TaskItemSearchModel filter)
    {
        var query = _taskContext.TaskItems
            .Include(x => x.TaskList)
             .AsQueryable();
        //var query = _taskContext.TaskItems.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(x => x.Title.Contains(filter.Title));

        if (filter.TaskListId > 0)
            query = query.Where(x => x.TaskListId == filter.TaskListId);

        //if (filter.IsDone.HasValue)
        //    query = query.Where(x => x.IsDone == filter.IsDone.HasValue);

        return await query
            .OrderByDescending(x => x.CreationDate)
            .ToListAsync();
    }

    //public List<TaskItem> Search(TaskItem searchModel)
    //{
    //    var query = _taskContext.TaskItems.Include(x => x.TaskList).Select(x => new TaskViewModel
    //    {
    //        Id = x.Id,
    //        Title = x.Title,
    //        IsDone = x.IsDone,
    //        TaskListId = x.TaskListId,
    //        TaskList = x.TaskList.Name,
    //        CreationDate = x.CreationDate.ToFarsi()
    //    });

    //    if (!string.IsNullOrWhiteSpace(searchModel.Name))
    //        query = query.Where(x => x.Title.Contains(searchModel.Name));

    //    if (searchModel.TaskListId != 0)
    //        query = query.Where(x => x.TaskListId == searchModel.TaskListId);

    //    return query.OrderByDescending(x => x.Id).ToList();
    //}

}
