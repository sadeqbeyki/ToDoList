using AppFramework.Application;
using System.Collections.Generic;

namespace ToDo.Application.Contracts.TaskCategory
{
    public interface ITaskCategoryApplication
    {
        OperationResult Create(CreateTaskCategory command);
        OperationResult Edit(EditTaskCategory command);
        EditTaskCategory GetDetails(long id);
        List<TaskCategoryViewModel> GetTaskCategories();
        List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel);
    }
}
