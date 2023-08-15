

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class OverwrittenRouteEndPoint 
{

  public string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}