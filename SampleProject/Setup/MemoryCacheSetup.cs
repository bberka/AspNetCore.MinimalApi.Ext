using Microsoft.Extensions.Caching.Memory;
using Selfrated.MinimalAPI.Middleware;

namespace SampleProject.Setup;

public class MemoryCacheSetup : IBuilderServiceSetup
{
    public void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
    {
        services.AddSingleton(e => new MemoryCache(
        new MemoryCacheOptions
        {
        }));
    }
}
