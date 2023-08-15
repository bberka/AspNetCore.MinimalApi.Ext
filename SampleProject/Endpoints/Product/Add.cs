using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;


[Endpoint(HttpMethodType.Delete)]
public class Add
{

  public bool Handle(HttpContext context, [FromBody] Classes.Product product)
  {
    var cache = context.RequestServices.GetRequiredService<MemoryCache>();
    var products = cache.Get<List<Classes.Product>>("Products") ?? new List<Classes.Product>();
    products.Add(product);
    cache.Set("Products", products);
    return true;
  }
}