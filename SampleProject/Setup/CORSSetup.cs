using AspNetCore.MinimalApi.Ext.Sample.Classes;
using AspNetCore.MinimalApi.Ext.Setup;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class CORSSetup : IApplicationSetup, IBuilderServiceSetup
{
  internal string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

  public int InitializationOrder { get; } = 1;
  public void InitializeApplication(WebApplication app)
  {
    app.UseCors(_myAllowSpecificOrigins);
  }

  public void InitializeServices(WebApplicationBuilder builder)
  {
    var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

    builder.Services.AddCors(options => {
      options.AddPolicy(_myAllowSpecificOrigins,
        builder => {
          builder.WithOrigins(appSettings?.CorsAllowedUrls.ToArray()).AllowAnyMethod().AllowAnyHeader();
        });
    });
  }
}