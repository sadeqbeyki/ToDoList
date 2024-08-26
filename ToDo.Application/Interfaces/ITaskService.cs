using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItems;


namespace ToDo.Application.Interfaces;

public interface ITaskService
{

    Task<List<TaskItemViewModel>> GetAllAsync();
    Task<TaskItemViewModel?> GetByIdAsync(long id);

    Task<OperationResult> Create(CreateTaskDto command);
    Task<OperationResult> Edit(EditTaskDto command);
    Task<List<TaskItemViewModel>> Search(TaskItemSearchModel filter);
    Task DeleteAsync(long id);

    Task ToggleIsDone(long id, bool isDone);

}

