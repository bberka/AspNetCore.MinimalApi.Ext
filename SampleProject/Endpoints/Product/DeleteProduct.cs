using Microsoft.Extensions.Caching.Memory;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

public class DeleteProduct : BaseEndpointSync.WithRequest<int>.WithResult<bool>
{
  public override bool Handle(HttpContext context, int request) {
    var cache = context.RequestServices.GetRequiredService<MemoryCache>();
    var products = cache.Get<List<Classes.Product>>("Products") ?? new List<Classes.Product>();
    var product = products.FirstOrDefault(p => p.Id == request);
    if (product == null) return false;
    products.Remove(product);
    cache.Set("Products", products);
    return true;
  }
}