using AspNetCore.MinimalApi.Ext.Setup;

namespace AspNetCore.MinimalApi.Ext.Sample.Setup;

public class EndpointsSetup : IApplicationSetup,IBuilderServiceSetup
{
  public int InitializationOrder => 1;
  public void InitializeApplication(WebApplication app)
  {
    app.UseMinimalApiEndpoints();
  }
  public void InitializeServices(WebApplicationBuilder builder)
  {
    builder.Services.AddMinimalApiEndpointOptions(x => {
      x.GlobalPrefix = "api/v1";
    });
  }
}