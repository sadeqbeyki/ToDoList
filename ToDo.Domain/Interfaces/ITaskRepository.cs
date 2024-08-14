using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskItem;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces;

public interface ITaskRepository : IRepository<long, TaskItem>
{
    Task<EditTask> GetTaskItemById(long id);
    Task<TaskItem> GetTaskItemWithTaskList(long id);
    List<TaskViewModel> Search(TaskSearchModel searchModel);
    Task<List<TaskViewModel>> GetAllTaskItem();
}
