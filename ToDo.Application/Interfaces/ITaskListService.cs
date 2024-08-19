using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskLists;

namespace ToDo.Application.Interfaces;

public interface ITaskListService
{
    Task<OperationResult> Create(CreateTaskListDto command);
    Task<OperationResult> Edit(EditTaskListDto command);
    Task<TaskListDto> GetDetails(long id);
    Task<List<TaskListDto>> GetAllTaskList();
    Task<List<TaskListDto>> Search(SearchTaskListDto searchModel);
}
