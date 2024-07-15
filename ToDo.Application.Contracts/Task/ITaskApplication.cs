using AppFramework.Application;
using ToDo.Application.Contracts.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Application.Contracts.Task;

public interface ITaskApplication
{
    OperationResult Create(CreateTask command);
    OperationResult Edit(EditTask command);
    EditTask GetDetails(long id);
    Task<TaskViewModel> GetTask(long id);
    List<TaskViewModel> Search(TaskSearchModel searchModel);
    List<TaskViewModel> GetTasks();
}
