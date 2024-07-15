using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.Task;

namespace ToDo.Domain.TaskAgg;

public interface ITaskRepository : IRepository<long, TaskItem>
{
    EditTask GetDetails(long id);
    Task<TaskViewModel> GetTask(long id);
    TaskItem GetTaskWithCategory(long id);
    List<TaskViewModel> Search(TaskSearchModel searchModel);
    List<TaskViewModel> GetTasks();
}
