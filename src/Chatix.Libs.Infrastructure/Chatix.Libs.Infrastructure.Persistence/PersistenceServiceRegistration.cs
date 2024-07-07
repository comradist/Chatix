using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.ConfigurationModels;
using Chatix.Libs.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Chatix.Libs.Infrastructure.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationConStrToDbNote configurationConStrToDbNote = configuration.GetSection(ConfigurationConStrToDbNote.Key).Get<ConfigurationConStrToDbNote>();
        services.AddDbContext<RepositoryChatixDbContext>(options =>
            options.UseMySql(configurationConStrToDbNote.SqlConnectionToAppDb, new MySqlServerVersion(new Version(8, 0, 26))));

        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IRoomRepository, RoomRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRepositoryManager, RepositoryManager>();


        return services;
    }
}