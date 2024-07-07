using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chatix.Service.App.Domain;

public static class DomainServicesRegistration
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}