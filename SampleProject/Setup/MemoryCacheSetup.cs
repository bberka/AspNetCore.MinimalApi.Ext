using AspNetCore.MinimalApi.Ext.Setup;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class MemoryCacheSetup : IBuilderServiceSetup
{
  public void InitializeServices(WebApplicationBuilder builder)
  {
    builder.Services.AddSingleton(e => new MemoryCache(
      new MemoryCacheOptions()));
  }
}