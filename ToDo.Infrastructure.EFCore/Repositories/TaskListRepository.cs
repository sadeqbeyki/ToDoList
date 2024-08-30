using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Domain.DTOs;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskListRepository : RepositoryBase<long, TaskList>, ITaskListRepository
{
    private readonly ToDoContext _todoContext;

    public TaskListRepository(ToDoContext todoContext) : base(todoContext)
    {
        _todoContext = todoContext;
    }

    public async Task<List<TaskList>> GetAllTaskLists()
    {
        return await _todoContext.TaskLists.ToListAsync();
    }

    public async Task<TaskList> GetDetails(long id)
    {
        return await _todoContext.TaskLists.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TaskListViewDto>> SearchAsync(TaskListSearchDto searchModel)
    {
        var query = _todoContext.TaskLists
            .Select(x => new TaskListViewDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));

        return await query.OrderByDescending(x => x.Id).ToListAsync();
    }
}
