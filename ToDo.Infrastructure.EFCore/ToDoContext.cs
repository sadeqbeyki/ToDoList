using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.EFCore.Mappings;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.EFCore;

public class ToDoContext : DbContext
{
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(TaskListConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
