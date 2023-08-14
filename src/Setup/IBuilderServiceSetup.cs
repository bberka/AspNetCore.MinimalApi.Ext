using Microsoft.AspNetCore.Builder;

namespace AspNetCore.MinimalApi.Ext.Setup;

public interface IBuilderServiceSetup
{
  /// <summary>
  ///   This is where you can add any service setup
  /// </summary>
  /// <param name="services"></param>
  void InitializeServices(WebApplicationBuilder builder);
}