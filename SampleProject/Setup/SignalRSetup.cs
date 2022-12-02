using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using SampleProject.Classes;
using SampleProject.Hubs;
using Selfrated.MinimalAPI.Middleware;

namespace SampleProject.Setup;

public class SignalRSetup : IApplicationSetup, IBuilderServiceSetup
{
    public void InitializeApplication(WebApplication app)
    {
        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapHub<ChatHub>("/chat");
    }

    public void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
    {
        services.AddSignalR();
    }
}
