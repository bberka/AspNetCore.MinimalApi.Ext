using AspNetCore.MinimalApi.Ext.Middleware;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class EndpointsSetup : IApplicationSetup
{
  public void InitializeApplication(WebApplication app) {
    app.UseMinimalApiEndpoints(x => { x.GlobalPrefix = "api"; });
  }
}