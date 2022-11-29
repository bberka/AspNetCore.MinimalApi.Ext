using Microsoft.OpenApi.Models;
using SampleProject.Classes;
using Selfrated.MinimalAPI.Middleware;

namespace SampleProject.Setup;

public class Swagger : IBuilderServiceSetup, IApplicationSetup
{
    internal readonly string _title = "Sample Project";
    internal readonly string _version = "v1";

    public void InitializeApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            var azureAd = app.Configuration.GetSection("AzureAd").Get<AzureAd>() ?? new ();
            var appSettings = app.Configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>() ?? new ();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{appSettings.Version}/swagger.json", appSettings.ApplicationName);

                //this is for Azure AD authentication
                c.OAuthClientId(azureAd.ClientId);
                c.OAuthUsePkce();
            });
        }
    }

    public void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
    {
        var azureAd = configuration.GetSection("AzureAd").Get<AzureAd>() ?? new ();
        var appSettings = configuration.GetSection("SwaggerSettings").Get<SwaggerSettings>() ?? new ();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(appSettings.Version,
                    new OpenApiInfo
                    {
                        Title = appSettings.ApplicationName,
                        Version = appSettings.Version
                    });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                    },
                    new List <string> ()
                }
            });

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"https://login.microsoftonline.com/{azureAd.TenantId}/oauth2/v2.0/authorize"),
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
        });
    }
}
