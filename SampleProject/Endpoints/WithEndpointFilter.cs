using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Sample.Classes;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[EndpointFilter(typeof(SampleEndpointFilter))]
public class WithEndpointFilter : BaseEndpointSync.WithRequest<string>.WithResult<string>
{
  public override string Handle(HttpContext context, string id)
  {
    return $"Hello {id}!";
  }
}