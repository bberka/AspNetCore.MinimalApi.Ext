using AspNetCore.MinimalApi.Ext.Middleware;
using AspNetCore.MinimalApi.Ext.Sample.Classes;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class CORSSetup : IApplicationSetup, IBuilderServiceSetup
{
  internal string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

  public void InitializeApplication(WebApplication app) {
    app.UseCors(_myAllowSpecificOrigins);
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host) {
    var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

    services.AddCors(options => {
      options.AddPolicy(_myAllowSpecificOrigins,
        builder => {
          builder.WithOrigins(appSettings.CORSAllowedURLS.ToArray()).AllowAnyMethod().AllowAnyHeader();
          ;
        });
    });
  }
}
