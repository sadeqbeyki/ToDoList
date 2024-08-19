using AppFramework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public List<TaskList> Search(TaskList searchModel)
    {
        var query = _todoContext.TaskLists.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchModel.Name))
            query = query.Where(x => x.Name.Contains(searchModel.Name));
        return query.OrderByDescending(x => x.Id).ToList();
    }
}
