using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;

namespace ToDo.Application.Interfaces;

public interface ITaskListService
{
    Task<OperationResult> Create(CreateTaskListDto command);
    Task<OperationResult> Edit(EditTaskListDto command);
    Task<TaskListViewModel> GetDetails(long id);
    Task<List<TaskListViewModel>> GetAllTaskList();
    Task<List<TaskListViewModel>> SearchAsync(TaskListSearchModel searchModel);
    Task DeleteAsync(long id);
}
