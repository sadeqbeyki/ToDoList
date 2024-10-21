using Microsoft.EntityFrameworkCore;
using ToDo.Infrastructure.EFCore.Mappings;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.EFCore.Persistence;

public class ToDoDbContext : DbContext
{
    public DbSet<TaskList> TaskLists { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(TaskListConfiguration).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}
