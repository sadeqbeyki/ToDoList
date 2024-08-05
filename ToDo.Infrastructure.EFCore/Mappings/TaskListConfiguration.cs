using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.EFCore.Mappings;

public class TaskListConfiguration : IEntityTypeConfiguration<TaskList>
{
    public void Configure(EntityTypeBuilder<TaskList> builder)
    {
        builder.ToTable("TaskList");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

        builder.Property(x => x.Description).HasMaxLength(500);

        builder.HasMany(x => x.TaskItems)
            .WithOne(x => x.TaskList)
            .HasForeignKey(x => x.TaskListId);
    }
}
