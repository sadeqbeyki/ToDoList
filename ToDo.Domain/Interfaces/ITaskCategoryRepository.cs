using AppFramework.Domain;
using System.Collections.Generic;
using ToDo.Application.Contracts.TaskCategory;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces
{
    public interface ITaskCategoryRepository : IRepository<long, TaskList>
    {
        EditTaskCategory GetDetails(long id);
        List<TaskCategoryViewModel> GetTaskCategories();
        List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel);
    }
}
