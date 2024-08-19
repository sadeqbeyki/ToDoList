using ToDo.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infrastructure.EFCore;
using ToDo.Domain.Interfaces;
using ToDo.Application.Services;
using ToDo.Application.Interfaces;
using ToDo.Application.Mapper;

namespace ToDo.Infrastructure.Configuration;

public class ToDoBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<ITaskListService, TaskListService>();
        services.AddTransient<ITaskListRepository, TaskListRepository>();

        services.AddTransient<ITaskService, TaskItemService>();
        services.AddTransient<ITaskRepository, TaskRepository>();

        services.AddAutoMapper(typeof(MappingProfile));


        services.AddDbContext<ToDoContext>(x => x.UseSqlServer(connectionString));
    }
}
