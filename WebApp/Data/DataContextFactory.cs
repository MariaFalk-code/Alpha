using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WebApp.Data;

namespace WebApp.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

        // Replace with your actual connection string
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProjectManagerDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new DataContext(optionsBuilder.Options);
    }
}

