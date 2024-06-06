using ToDo.Domain.TaskAgg;
using ToDo.Domain.TaskCategoryAgg;
using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.EFCore.Mappings;

namespace ToDo.Infrastructure.EFCore
{
    public class ToDoContext : DbContext
    {
        public DbSet<TaskCategory> TaskViewModel { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(TaskCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
