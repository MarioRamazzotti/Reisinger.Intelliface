
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reisinger_Intelliface_1_0.Data;

internal class IntellifaceContextFactory : IDesignTimeDbContextFactory<IntellifaceDataContext>
{
    public IntellifaceDataContext CreateDbContext(string[] args)
    {
        IConfiguration configurations = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

        var connectionString = configurations.GetConnectionString("EmployeeDB");

        var optionsBuilder = new DbContextOptionsBuilder<IntellifaceDataContext>();
        optionsBuilder.UseSqlite(connectionString);


        return new IntellifaceDataContext(optionsBuilder.Options, configurations);
    }
}