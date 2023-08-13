using AspNetCore.MinimalApi.Ext.Sample.Hubs;
using AspNetCore.MinimalApi.Ext.Setup;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class SignalRSetup : IApplicationSetup, IBuilderServiceSetup
{
  public int InitializationOrder { get; } = 1;
  public void InitializeApplication(WebApplication app)
  {
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapHub<ChatHub>("/chat");
  }

  public void InitializeServices(WebApplicationBuilder builder)
  {
    builder.Services.AddSignalR();
  }
}