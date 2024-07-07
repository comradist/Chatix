using Chatix.Libs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace OutOfOffice.Persistence.ContextFactory;

public class RepositoryAppDbContextFactory : IDesignTimeDbContextFactory<RepositoryChatixDbContext>
{
    public RepositoryChatixDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Chatix.Service.App/Chatix.Service.App.API"))
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<RepositoryChatixDbContext>();
        builder.UseMySql(configuration.GetConnectionString("SqlConnectionToAppDb"), new MySqlServerVersion(new Version(8, 0, 26)));


        return new RepositoryChatixDbContext(builder.Options);
    }
}


