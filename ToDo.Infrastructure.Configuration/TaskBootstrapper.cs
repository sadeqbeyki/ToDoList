using ToDo.Application;
using ToDo.Application.Contracts.Task;
using ToDo.Application.Contracts.TaskCategory;
using ToDo.Domain.TaskAgg;
using ToDo.Domain.TaskCategoryAgg;
using ToDo.Infrastructure.EFCore;
using ToDo.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Infrastructure.EFCore.Repositories;

namespace ToDo.Infrastructure.Configuration
{
    public class TaskBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ITaskCategoryApplication, TaskCategoryApplication>();
            services.AddTransient<ITaskCategoryRepository, TaskCategoryRepository>();

            services.AddTransient<ITaskApplication, TaskApplication>();
            services.AddTransient<ITaskRepository, TaskRepository>();

            services.AddDbContext<TodoContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
