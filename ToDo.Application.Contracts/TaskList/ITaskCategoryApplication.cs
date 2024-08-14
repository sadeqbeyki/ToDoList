using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Application.Contracts.TaskList;

public interface ITaskCategoryApplication
{
    Task<OperationResult> Create(CreateTaskCategory command);
    Task<OperationResult> Edit(EditTaskCategory command);
    Task<EditTaskCategory> GetDetails(long id);
    Task<List<TaskCategoryViewModel>> GetTaskList();
    List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel);
}
