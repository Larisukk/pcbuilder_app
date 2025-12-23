using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pcbuilder.Api.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // Use the connection string from appsettings.json for design-time
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=pcbd;Username=pcbd;Password=pcbd");
        
        return new AppDbContext(optionsBuilder.Options);
    }
}
