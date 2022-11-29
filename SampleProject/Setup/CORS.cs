using SampleProject.Classes;
using Selfrated.MinimalAPI.Middleware;

namespace SampleProject.Setup;

public class CORS : IApplicationSetup, IBuilderServiceSetup
{
    internal string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public void InitializeApplication(WebApplication app)
    {
        app.UseCors(_myAllowSpecificOrigins);
    }

    public void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
    {
        var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: _myAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins(appSettings.CORSAllowedURLS.ToArray()).AllowAnyMethod().AllowAnyHeader();
                    ;
                });
        });
    }
}
