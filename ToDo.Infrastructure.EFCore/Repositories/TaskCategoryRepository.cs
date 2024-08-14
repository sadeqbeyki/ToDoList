using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Application.Contracts.TaskList;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskCategoryRepository : RepositoryBase<long, TaskList>, ITaskCategoryRepository
{
    private readonly ToDoContext _todoContext;

    public TaskCategoryRepository(ToDoContext todoContext) : base(todoContext)
    {
        _todoContext = todoContext;
    }

    public async Task<List<TaskCategoryViewModel>> GetTaskCategories()
    {
        return await _todoContext.TaskList.Select(x => new TaskCategoryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToFarsi()
        }).ToListAsync();
    }

    public async Task<EditTaskCategory> GetDetails(long id)
    {
        return await _todoContext.TaskList.Select(x => new EditTaskCategory
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
        }).FirstOrDefaultAsync(x => x.Id == id);
    }

    public List<TaskCategoryViewModel> Search(TaskCategorySearchModel searchModel)
    {
        var query = _todoContext.TaskList.Select(x => new TaskCategoryViewModel
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToFarsi()
        });
        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));
        return query.OrderByDescending(x => x.Id).ToList();
    }
}
