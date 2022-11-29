using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Selfrated.MinimalAPI.Middleware;

public static class SetupBuilderMiddlewareExtensions
{
    /// <summary>
    /// This will enable any classes implementing IBuilderServiceSetup
    /// </summary>
    /// <param name="builder"></param>
    public static void UseBuilderSetup(this WebApplicationBuilder builder)
    {
        var results = Assembly.GetCallingAssembly().ExportedTypes
                    .Where(x => typeof(IBuilderServiceSetup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IBuilderServiceSetup>();

        foreach (var result in results)
        {
            result.InitializeServices(builder.Services, builder.Configuration, builder.Host);
        }
    }
}

public interface IBuilderServiceSetup
{
    /// <summary>
    /// This is where you can add any service setup
    /// </summary>
    /// <param name="services"></param>
    void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host);
}