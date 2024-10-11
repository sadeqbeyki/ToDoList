using AppFramework.Application;
using AppFramework.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDo.Domain.Entities;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infrastructure.EFCore.Persistance;

namespace ToDo.Infrastructure.EFCore.Repositories;

public class TaskRepository : RepositoryBase<long, TaskItem>, ITaskRepository
{
    private readonly ToDoDbContext _taskContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaskRepository(ToDoDbContext taskContext, IHttpContextAccessor httpContextAccessor) : base(taskContext)
    {
        _taskContext = taskContext;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<List<TaskItem>> GetAllTaskItem()
        => await _taskContext.TaskItems.ToListAsync();

    public async Task<TaskItem> GetTaskItemById(long id)
        => await _taskContext.TaskItems
                 .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<TaskItem> GetTaskItemWithListName(long id)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _taskContext.TaskItems
                     .Include(t => t.TaskList)
                     .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }


    public async Task<List<TaskItemViewDto>> Search(TaskItemSearchDto searchModel)
    {
        var query = _taskContext.TaskItems
            .Include(x => x.TaskList)
            .Select(x => new TaskItemViewDto
            {
                Id = x.Id,
                Title = x.Title,
                IsDone = x.IsDone,
                Description = x.Description,
                TaskListId = x.TaskListId,
                TaskListTitle = x.TaskList.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchModel.Title))
            query = query.Where(x => x.Title.Contains(searchModel.Title));

        if (searchModel.TaskListId > 0)
            query = query.Where(x => x.TaskListId == searchModel.TaskListId);

        if (searchModel.IsDone.HasValue)
            query = query.Where(x => x.IsDone == searchModel.IsDone.Value);

        return await query.OrderByDescending(x => x.Id).ToListAsync();
    }

    public IQueryable<TaskItem> GetQueryable()
    {
        return _taskContext.TaskItems
            .Include(x => x.TaskList)
            .AsNoTracking();
    }
}
