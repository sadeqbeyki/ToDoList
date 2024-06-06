using AppQuery.Contracts.Task;
using AppQuery.Contracts.TaskCategory;
using ToDo.Domain.TaskAgg;
using ToDo.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppQuery.Query
{
    public class TaskCategoryQuery : ITaskCategoryQuery
    {
        private readonly ToDoContext _todoContext;

        public TaskCategoryQuery(ToDoContext todoContext)
        {
            _todoContext = todoContext;
        }

        public List<TaskCategoryQueryModel> GetTaskCategories()
        {
            return _todoContext.TaskViewModel.Select(x => new TaskCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<TaskCategoryQueryModel> GetTaskCategoriesWithTasks()
        {
            var categories = _todoContext.TaskViewModel.Include(x => x.Tasks).ThenInclude(x => x.Category)
                .Select(x => new TaskCategoryQueryModel
                {
                    Id=x.Id,
                    Name=x.Name,
                    Tasks=MapTasks(x.Tasks)
                }).OrderByDescending(x => x.Id).ToList();
            return categories;
        }
        private static List<TaskQueryModel> MapTasks(List<TaskItem> products)
        {
            return products.Select(p => new TaskQueryModel
            {
                Id = p.Id,
                Category = p.Category.Name,
                Name = p.Name,
            }).ToList();
        }
    }
}
