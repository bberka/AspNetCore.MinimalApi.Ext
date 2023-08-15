using AspNetCore.MinimalApi.Ext.Sample.Classes;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[EndpointFilter(typeof(SampleEndpointFilter))]
[Endpoint]
public class WithEndpointFilter 
{
  public string Handle(HttpContext context, string id)
  {
    return $"Hello {id}!";
  }
}