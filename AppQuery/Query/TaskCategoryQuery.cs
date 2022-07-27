using AppQuery.Contracts.Task;
using AppQuery.Contracts.BookCategory;
using Library.Domain.BookAgg;
using Library.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppQuery.Query
{
    public class TaskCategoryQuery : ITaskCategoryQuery
    {
        private readonly LibraryContext _libraryContext;

        public TaskCategoryQuery(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public List<TaskCategoryQueryModel> GetTaskCategories()
        {
            return _libraryContext.TaskViewModel.Select(x => new TaskCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<TaskCategoryQueryModel> GetBookCategoriesWithBooks()
        {
            var categories = _libraryContext.TaskViewModel.Include(x => x.Tasks).ThenInclude(x => x.Category)
                .Select(x => new TaskCategoryQueryModel
                {
                    Id=x.Id,
                    Name=x.Name,
                    Tasks=MapBooks(x.Tasks)
                }).OrderByDescending(x => x.Id).ToList();
            return categories;
        }
        private static List<TaskQueryModel> MapBooks(List<Task> products)
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
