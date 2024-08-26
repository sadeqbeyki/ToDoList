using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Models;

namespace ToDo.Domain.Interfaces;

public interface ITaskRepository : IRepository<long, TaskItem>
{
    Task<TaskItem> GetTaskItemById(long id);
    Task<List<TaskItem>> GetAllTaskItem();
    Task<TaskItem> GetTaskItemWithTaskList(long id);
    Task<List<TaskItemDto>> Search(TaskItemSearchDto searchModel);
}
