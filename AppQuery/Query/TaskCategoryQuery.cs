using AppQuery.Contracts.Task;
using AppQuery.Contracts.TaskCategory;
using ToDo.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToDo.Domain.Entities;

namespace AppQuery.Query;

public class TaskCategoryQuery : ITaskCategoryQuery
{
    private readonly ToDoContext _todoContext;

    public TaskCategoryQuery(ToDoContext todoContext)
    {
        _todoContext = todoContext;
    }

    public List<TaskCategoryQueryModel> GetTaskCategories()
    {
        return _todoContext.TaskList.Select(x => new TaskCategoryQueryModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public List<TaskCategoryQueryModel> GetTaskCategoriesWithTasks()
    {
        var categories = _todoContext.TaskList.Include(x => x.TaskItems).ThenInclude(x => x.TaskList)
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
