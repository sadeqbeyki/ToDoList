using AppFramework.Application;
using AppFramework.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using ToDo.Application.Contracts.TaskCategory;
using ToDo.Domain.TaskCategoryAgg;
using ToDo.Infrastructure.EFCore;

namespace ToDo.Infrastructure.EFCore.Repositories
{
    public class TaskCategoryRepository : RepositoryBase<long, TaskCategory>, ITaskCategoryRepository
    {
        private readonly ToDoContext _todoContext;

        public TaskCategoryRepository(ToDoContext todoContext) : base(todoContext)
        {
            _todoContext = todoContext;
        }

        public List<TaskCategoryViewModel> GetTaskCategories()
        {
            return _todoContext.TaskViewModel.Select(x => new TaskCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }

        public EditTaskCategory GetDetails(long id)
        {
            return _todoContext.TaskViewModel.Select(x => new EditTaskCategory
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel)
        {
            var query = _todoContext.TaskViewModel.Select(x => new TaskCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
