using AppFramework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.DTOs.TaskItem;
using ToDo.Application.DTOs.TaskItems;

namespace ToDo.Application.Interfaces;

public interface ITaskService
{

    Task<List<TaskItemDto>> GetAllAsync();
    Task<TaskItemDto?> GetByIdAsync(long id);

    Task<OperationResult> Create(CreateTaskDto command);
    Task<OperationResult> Edit(EditTaskDto command);

    Task<List<TaskItemDto>> Search(SearchTaskItemDto searchModel);
    Task DeleteAsync(long id);

    Task ToggleIsDone(long id, bool isDone);

}

