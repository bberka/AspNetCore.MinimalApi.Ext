using AspNetCore.MinimalApi.Ext.Middleware;
using AspNetCore.MinimalApi.Ext.Sample.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class AzureADAuthenticationSetup : IApplicationSetup, IBuilderServiceSetup
{
  public void InitializeApplication(WebApplication app) {
    var azureAd = app.Configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null) {
      app.UseAuthentication();
      app.UseAuthorization();
    }
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host) {
    var azureAd = configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null)
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options => { configuration.Bind("AzureAd", options); },
          options => { configuration.Bind("AzureAd", options); });
  }
}
