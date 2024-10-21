using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ToDo.Infrastructure.EFCore.Persistence;

public class ToDoDbContextFactory : IDesignTimeDbContextFactory<ToDoDbContext>
{
    public ToDoDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>();
        //optionsBuilder.UseSqlServer("Server=.;Database=ToDoDB;Trusted_Connection=True;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=ToDoDB; Persist Security Info=True; TrustServerCertificate=True; Trusted_Connection=True; MultipleActiveResultSets=true; User ID=sa; Password=7410");

        return new ToDoDbContext(optionsBuilder.Options);
    }
}
