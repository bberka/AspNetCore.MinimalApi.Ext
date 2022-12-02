using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using SampleProject.Hubs;
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace SampleProject.Endpoints;

[EndpointAPI]
public class ProductsEndpoint
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    internal string _cacheKey = "Products";

    [EndpointMethod(RouteType = RouteType.GET, AuthenticationRequired = AuthenticationRequired.Yes)]
    public IResult GetAllProducts(MemoryCache cache)
    {
        var products = cache.Get<List<Product>>(_cacheKey) ?? new();

        return Results.Ok(products);
    }

    [EndpointMethod(RouteType = RouteType.DELETE, AuthenticationRequired = AuthenticationRequired.Yes)]
    public IResult DeleteProduct(MemoryCache cache, int id)
    {
        var products = cache.Get<List<Product>>(_cacheKey) ?? new();
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return Results.NotFound();
        }

        products.Remove(product);
        cache.Set(_cacheKey, products);
        return Results.Ok();
    }

    [EndpointMethod]
    public async Task<IResult> AddProduct(MemoryCache cache, IHubContext<ChatHub> hubContext, string name)
    {
        var products = cache.Get<List<Product>>(_cacheKey);

        if (products == null)
        {
            products = new List<Product>();
        }

        var product = new Product
        {
            Id = products.Count == 0 ? 1 : products.Max(e => e.Id) + 1,
            Name = name
        };

        products.Add(product);

        await hubContext.Clients.All.SendAsync("ProdcutAdded", name);

        cache.Set(_cacheKey, products);

        return Results.Ok(product);
    }
}
