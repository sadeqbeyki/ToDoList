using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Models;

namespace ToDo.Domain.Interfaces;

public interface ITaskRepository : IRepository<long, TaskItem>
{
    Task<TaskItem> GetTaskItemById(long id);
    Task<TaskItem> GetTaskItemWithTaskList(long id);
    Task<List<TaskItem>> SearchAsync(TaskItemSearchModel searchModel);
    Task<List<TaskItem>> GetAllTaskItem();
}
