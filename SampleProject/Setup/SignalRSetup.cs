using AspNetCore.MinimalApi.Ext.Middleware;
using AspNetCore.MinimalApi.Ext.Sample.Hubs;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class SignalRSetup : IApplicationSetup, IBuilderServiceSetup
{
  public void InitializeApplication(WebApplication app) {
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapHub<ChatHub>("/chat");
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host) {
    services.AddSignalR();
  }
}
