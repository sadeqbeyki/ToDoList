using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.DTOs;
using ToDo.Domain.Entities;
using ToDo.Domain.Models;

namespace ToDo.Domain.Interfaces;

public interface ITaskListRepository : IRepository<long, TaskList>
{
    Task<TaskList> GetDetails(long id);
    Task<List<TaskList>> GetAllTaskLists();
    Task<List<TaskListViewDto>> SearchAsync(TaskListSearchDto searchModel);
}
