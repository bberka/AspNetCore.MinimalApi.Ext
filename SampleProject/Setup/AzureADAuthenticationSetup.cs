using AspNetCore.MinimalApi.Ext.Sample.Classes;
using AspNetCore.MinimalApi.Ext.Setup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class AzureADAuthenticationSetup : IApplicationSetup, IBuilderServiceSetup
{
  public int InitializationOrder { get; } = 1;
  public void InitializeApplication(WebApplication app)
  {
    var azureAd = app.Configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null) {
      app.UseAuthentication();
      app.UseAuthorization();
    }
  }

  public void InitializeServices(WebApplicationBuilder builder)
  {
    var azureAd = builder.Configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null)
      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options => { builder.Configuration.Bind("AzureAd", options); },
          options => { builder.Configuration.Bind("AzureAd", options); });
  }
}