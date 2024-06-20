using AppFramework.Domain;
using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;

namespace ToDo.Domain.TaskCategoryAgg
{
    public interface ITaskCategoryRepository : IRepository<long, TaskList>
    {
        EditTaskCategory GetDetails(long id);
        List<TaskCategoryViewModel> GetTaskCategories();
        List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel);
    }
}
