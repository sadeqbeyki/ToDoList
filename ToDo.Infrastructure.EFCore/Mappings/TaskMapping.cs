using ToDo.Domain.TaskAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Infrastructure.EFCore.Mappings
{
    public class TaskMapping : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("tasks");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).HasMaxLength(16).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Author).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Publisher).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Translator).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasOne(x => x.Category).WithMany(x => x.Tasks).HasForeignKey(x => x.CategoryId);
        }
    }
}
