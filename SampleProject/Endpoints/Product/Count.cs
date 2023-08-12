using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[EndpointHttpMethod(HttpMethodTypes.POST)]
public class Count : BaseEndpointSync.WithoutRequest.WithResult<int>
{
  public override int Handle(HttpContext context)
  {
    return new Random().Next();
  }
}