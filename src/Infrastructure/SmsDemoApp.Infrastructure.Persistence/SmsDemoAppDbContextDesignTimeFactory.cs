using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmsDemoApp.Infrastructure.Persistence;

public class SmsDemoAppDbContextDesignTimeFactory:IDesignTimeDbContextFactory<SmsDemoAppDbContext>
{
    public SmsDemoAppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<SmsDemoAppDbContext>()
            .UseSqlServer("Server=.;Database=SmsDemoApp;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false");

        return new SmsDemoAppDbContext(optionBuilder.Options);
    }
}