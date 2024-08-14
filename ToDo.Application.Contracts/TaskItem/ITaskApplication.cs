using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Application.Contracts.TaskItem;

public interface ITaskApplication
{
    Task<OperationResult> Create(CreateTask command);
    Task<OperationResult> Edit(EditTask command);
    Task<EditTask> GetDetails(long id);
    List<TaskViewModel> Search(TaskSearchModel searchModel);
    Task<List<TaskViewModel>> GetTasks();
    Task ToggleIsDone(long id, bool isDone);
}
