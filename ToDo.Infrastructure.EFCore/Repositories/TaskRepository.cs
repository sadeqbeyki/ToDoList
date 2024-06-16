using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.Application.Contracts.Task;
using ToDo.Domain.TaskAgg;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskRepository : RepositoryBase<long, TaskItem>, ITaskRepository
{
    private readonly ToDoContext _taskContext;

    public TaskRepository(ToDoContext taskContext) : base(taskContext)
    {
        _taskContext = taskContext;
    }

    public List<TaskViewModel> GetTasks()
    {
        return _taskContext.Tasks.Select(x => new TaskViewModel
        {
            Id = x.Id,
            Title = x.Title,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToList();
    }

    public TaskItem GetTaskWithCategory(long id)
    {
        return _taskContext.Tasks.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }

    public EditTask GetDetails(long id)
    {
        return _taskContext.Tasks.Select(x => new EditTask
        {
            Id = x.Id,
            Title = x.Title,
            CategoryId = x.CategoryId,
            Description = x.Description
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<TaskViewModel> Search(TaskSearchModel searchModel)
    {
        var query = _taskContext.Tasks.Include(x => x.Category).Select(x => new TaskViewModel
        {
            Id = x.Id,
            Title = x.Title,
            CategoryId = x.CategoryId,
            Category = x.Category.Name,
            CreationDate = x.CreationDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Title.Contains(searchModel.Name));

        if (searchModel.CategoryId != 0)
            query = query.Where(x => x.CategoryId == searchModel.CategoryId);

        return query.OrderByDescending(x => x.Id).ToList();
    }

}
