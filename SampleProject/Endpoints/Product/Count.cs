
namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product;

[Endpoint(HttpMethodType.Get)]
public sealed class Count : BaseEndpoint
{
  public int Handle(HttpContext context) => new Random().Next();
}