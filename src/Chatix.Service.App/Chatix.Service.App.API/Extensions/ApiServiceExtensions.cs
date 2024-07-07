using Chatix.Service.App.API.Presentation.ActionFilters;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Chatix.Service.App.API.Extensions;

public static class ApiServiceRegistration
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Chatix.Api", Version = "v1" });
        });
    }

    public static void ConfigureCQRS(this IServiceCollection services)
    {
        services.AddCors(parameter =>
        {
            parameter.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    public static void ConfigureValidationFilterAttribute(this IServiceCollection services) =>
        services.AddScoped<ValidationFilterAttribute>();


    // public static OptionsBuilder<JwtConfiguration> ConfigureOptionsJwt(this IServiceCollection services, IConfiguration configuration)
    // {
    //     return services.AddOptions<JwtConfiguration>()
    //                 .Bind(configuration.GetSection(JwtConfiguration.Key))
    //                 .ValidateDataAnnotations()
    //                 .ValidateOnStart();
    // }

}