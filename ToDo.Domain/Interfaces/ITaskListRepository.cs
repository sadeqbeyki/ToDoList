using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces;

public interface ITaskListRepository : IRepository<long, TaskList>
{
    Task<TaskList> GetDetails(long id);
    Task<List<TaskList>> GetAllTaskLists();
    List<TaskList> Search(TaskList searchModel);
}
