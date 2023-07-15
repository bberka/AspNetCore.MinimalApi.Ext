using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[EndpointAuthorize]
[EndpointRoute("TestRoute")]
[EndpointMethod(HttpMethodTypes.POST)]
public class Count : BaseEndpointSync.WithoutRequest.WithResult<int>
{
  [HttpGet]
  public override int Handle(HttpContext context) {
    return new Random().Next();
  }
}