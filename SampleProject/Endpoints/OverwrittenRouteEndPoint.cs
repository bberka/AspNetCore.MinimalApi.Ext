using AspNetCore.MinimalApi.Ext.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[EndpointRoute("NameOverride")]
public class OverwrittenRouteEndPoint : BaseEndpointSync.WithoutRequest.WithResult<string>
{
  [HttpPost]
  public override string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}