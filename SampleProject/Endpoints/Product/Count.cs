using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[EndpointMethod(HttpMethodType.POST)]
public class Count : BaseEndpoint
{
  public int Handle(HttpContext context)
  {
    return new Random().Next();
  }
}