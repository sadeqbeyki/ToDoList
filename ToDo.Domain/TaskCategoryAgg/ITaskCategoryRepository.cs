using AppFramework.Domain;
using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;

namespace ToDo.Domain.TaskCategoryAgg
{
    public interface ITaskCategoryRepository : IRepository<long, TaskCategory>
    {
        EditTaskCategory GetDetails(long id);
        List<TaskCategorySearchModel> GetTaskCategories();
        List<TaskCategorySearchModel> Search(TaskCategorySearchModel searchModel);
    }
}
