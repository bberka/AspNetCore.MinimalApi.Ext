using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[EndpointRoute("NameOverride")]
[EndpointMethod(HttpMethodType.POST)]
public class OverwrittenRouteEndPoint : BaseEndpoint
{
  public string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}