using ToDo.Domain.TaskAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Infrastructure.EFCore.Mappings;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x=>x.IsDone)
            .IsRequired();

        builder.Property(x=>x.TaskListId)
            .IsRequired();

        builder.HasOne(x => x.TaskList)
            .WithMany(x => x.TaskItems)
            .HasForeignKey(x => x.TaskListId);
    }
}
