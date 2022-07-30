using System.Collections.Generic;

namespace AppQuery.Contracts.TaskCategory
{
    public interface ITaskCategoryQuery
    {
        List<TaskCategoryQueryModel> GetTaskCategories();
        List<TaskCategoryQueryModel> GetTaskCategoriesWithTasks();
    }
}
