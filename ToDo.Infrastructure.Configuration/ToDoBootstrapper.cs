using ToDo.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Interfaces;
using ToDo.Application.Services;
using ToDo.Application.Interfaces;
using ToDo.Application.Mapper;
using ToDo.Infrastructure.EFCore.Persistance;

namespace ToDo.Infrastructure.Configuration;

public class ToDoBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        // Application & Domain Layer
        services.AddTransient<ITaskListService, TaskListService>();
        services.AddTransient<ITaskService, TaskItemService>();

        // Infrastructure Layer
        services.AddTransient<ITaskListRepository, TaskListRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();

        // AutoMapper Configuration
        services.AddAutoMapper(typeof(MappingProfile));

        // DbContext Configuration
        services.AddDbContext<ToDoDbContext>(options =>
            options.UseSqlServer(connectionString));
    }
}
