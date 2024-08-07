using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Task;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces;

public interface ITaskRepository : IRepository<long, TaskItem>
{
    EditTask GetDetails(long id);
    Task<TaskViewModel> GetByIdAsync(long id);
    TaskItem GetTaskWithCategory(long id);
    List<TaskViewModel> Search(TaskSearchModel searchModel);
    Task<List<TaskViewModel>> GetAllAsync();

}
