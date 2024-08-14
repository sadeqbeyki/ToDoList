using AppFramework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskList;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Interfaces;

public interface ITaskCategoryRepository : IRepository<long, TaskList>
{
    Task<EditTaskCategory> GetDetails(long id);
    Task<List<TaskCategoryViewModel>> GetTaskCategories();
    List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel);
}
