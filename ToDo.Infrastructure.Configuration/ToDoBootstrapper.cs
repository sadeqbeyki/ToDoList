using ToDo.Application;
using ToDo.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infrastructure.EFCore;
using ToDo.Domain.Interfaces;
using ToDo.Application.Contracts.TaskItem;
using ToDo.Application.Contracts.TaskList;

namespace ToDo.Infrastructure.Configuration
{
    public class ToDoBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ITaskCategoryApplication, TaskCategoryApplication>();
            services.AddTransient<ITaskCategoryRepository, TaskCategoryRepository>();

            services.AddTransient<ITaskApplication, TaskApplication>();
            services.AddTransient<ITaskRepository, TaskRepository>();

            services.AddDbContext<ToDoContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
