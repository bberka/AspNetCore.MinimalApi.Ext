using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[EndpointRoute("NameOverride")]
[EndpointMethod(HttpMethodType.POST)]
public class OverwrittenRouteEndPoint : BaseEndpointSync.WithoutRequest.WithResult<string>
{
  public override string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}