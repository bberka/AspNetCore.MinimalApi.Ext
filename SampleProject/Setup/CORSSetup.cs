using AspNetCore.MinimalApi.Ext.Sample.Classes;
using AspNetCore.MinimalApi.Ext.Setup;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class CORSSetup : IApplicationSetup, IBuilderServiceSetup
{
  internal string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

  public void InitializeApplication(WebApplication app)
  {
    app.UseCors(_myAllowSpecificOrigins);
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host)
  {
    var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

    services.AddCors(options => {
      options.AddPolicy(_myAllowSpecificOrigins,
        builder => {
          builder.WithOrigins(appSettings.CorsAllowedUrls.ToArray()).AllowAnyMethod().AllowAnyHeader();
          ;
        });
    });
  }
}