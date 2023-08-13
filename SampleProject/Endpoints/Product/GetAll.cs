using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[EndpointMethod(HttpMethodType.GET)]
public class GetAll : BaseEndpointSync.WithoutRequest.WithResult<List<Classes.Product>>
{
  public override List<Classes.Product> Handle(HttpContext context)
  {
    var _memoryCache = context.RequestServices.GetRequiredService<MemoryCache>();
    var products = _memoryCache.Get<List<Classes.Product>>("Products");
    return products ?? new List<Classes.Product>();
  }
}