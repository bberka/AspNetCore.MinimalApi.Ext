using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[EndpointHttpMethod(HttpMethodTypes.POST)]
public class Add : BaseEndpointSync.WithRequest<Classes.Product>.WithResult<bool>
{
  public override bool Handle(HttpContext context, [FromBody] Classes.Product product) {
    var cache = context.RequestServices.GetRequiredService<MemoryCache>();
    var products = cache.Get<List<Classes.Product>>("Products") ?? new List<Classes.Product>();
    products.Add(product);
    cache.Set("Products", products);
    return true;
  }
}