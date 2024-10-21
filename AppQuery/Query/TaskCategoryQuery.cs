using AppQuery.Contracts.Task;
using AppQuery.Contracts.TaskCategory;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.EFCore.Persistence;

namespace AppQuery.Query;

public class TaskCategoryQuery : ITaskCategoryQuery
{
    private readonly ToDoDbContext _todoContext;

    public TaskCategoryQuery(ToDoDbContext todoContext)
    {
        _todoContext = todoContext;
    }

    public List<TaskCategoryQueryModel> GetTaskCategories()
    {
        return _todoContext.TaskLists.Select(x => new TaskCategoryQueryModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public List<TaskCategoryQueryModel> GetTaskCategoriesWithTasks()
    {
        var categories = _todoContext.TaskLists.Include(x => x.TaskItems).ThenInclude(x => x.TaskList)
            .Select(x => new TaskCategoryQueryModel
            {
                Id=x.Id,
                Name=x.Name,
                Tasks=MapTasks(x.TaskItems)
            }).OrderByDescending(x => x.Id).ToList();
        return categories;
    }
    private static List<TaskQueryModel> MapTasks(List<TaskItem> products)
    {
        return products.Select(p => new TaskQueryModel
        {
            Id = p.Id,
            TaskList = p.TaskList.Name,
            Name = p.Title,
        }).ToList();
    }
}
