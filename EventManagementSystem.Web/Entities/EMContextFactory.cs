using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventManagementSystem.Web.Entities
{
    public class EMContextFactory : IDesignTimeDbContextFactory<EmContext>
    {
        public EmContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var dbContextBuilder = new DbContextOptionsBuilder<EmContext>();

            var connectionString = configuration.GetConnectionString("Db");

            return new EmContext(dbContextBuilder.UseNpgsql(connectionString).Options);
        }
    }
}
