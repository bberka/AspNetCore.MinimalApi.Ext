using AspNetCore.MinimalApi.Ext.Setup;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class MemoryCacheSetup : IBuilderServiceSetup
{
  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host)
  {
    services.AddSingleton(e => new MemoryCache(
      new MemoryCacheOptions()));
  }
}