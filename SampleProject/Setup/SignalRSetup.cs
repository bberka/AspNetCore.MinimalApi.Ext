using AspNetCore.MinimalApi.Ext.Sample.Hubs;
using AspNetCore.MinimalApi.Ext.Setup;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class SignalRSetup : IApplicationSetup, IBuilderServiceSetup
{
  public void InitializeApplication(WebApplication app)
  {
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapHub<ChatHub>("/chat");
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host)
  {
    services.AddSignalR();
  }
}