using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Chatix.Libs.Infrastructure.Logger.Logger;
using Chatix.Libs.Core.Shared.ConfigurationModels;
using Chatix.Libs.Core.Contracts.Logger;

namespace Chatix.Libs.Infrastructure.Logger;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationLogger configurationLogger = configuration.GetSection(ConfigurationLogger.Key).Get<ConfigurationLogger>();
        LogManager.Setup().LoadConfigurationFromFile(string.Concat(configurationLogger.PathToLog, configurationLogger.FileToLog));

        services.AddSingleton<ILoggerManager, LoggerManager>();

        return services;
    }
}