using AspNetCore.MinimalApi.Ext.Sample.Classes;
using AspNetCore.MinimalApi.Ext.Setup;
using Microsoft.OpenApi.Models;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class SwaggerSetup : IBuilderServiceSetup, IApplicationSetup
{
  public void InitializeApplication(WebApplication app)
  {
    var swaggerSettings =
      app.Configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>() ?? new SwaggerSettings();

    if (app.Environment.IsDevelopment() && swaggerSettings != null) {
      var azureAd = app.Configuration.GetSection("AzureAd").Get<AzureAd>();

      app.UseSwagger();

      app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerSettings.ApplicationName);

        if (azureAd != null) {
          c.OAuthClientId(azureAd.ClientId);
          c.OAuthUsePkce();
        }
      });
    }
  }

  public void InitializeServices(IServiceCollection services, ConfigurationManager configuration,
    ConfigureHostBuilder host)
  {
    var swaggerSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>() ?? new SwaggerSettings();

    if (swaggerSettings != null) {
      var azureAd = configuration.GetSection("AzureAd").Get<AzureAd>();

      services.AddEndpointsApiExplorer();

      services.AddSwaggerGen(c => {
        c.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
        {
          Title = swaggerSettings.ApplicationName,
          Version = swaggerSettings.Version
        });

        if (azureAd != null) {
          c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
            {
              new OpenApiSecurityScheme
              {
                Reference = new OpenApiReference
                {
                  Type = ReferenceType.SecurityScheme,
                  Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
              },
              new List<string>()
            }
          });

          c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
          {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
              AuthorizationCode = new OpenApiOAuthFlow
              {
                AuthorizationUrl =
                  new Uri($"https://login.microsoftonline.com/{azureAd.TenantId}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"https://login.microsoftonline.com/{azureAd.TenantId}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                  {
                    azureAd.Scope,
                    "https://graph.microsoft.com/v1.0/me"
                  }
                }
              }
            }
          });
        }
      });
    }
  }
}