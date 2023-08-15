

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class OverwrittenRouteEndPoint : BaseEndpoint
{

  public string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}