using AspNetCore.MinimalApi.Ext.Sample.Classes;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class MyEndpointsContainer: EndpointContainer
{
  [Endpoint("/getRemoteIpAddress")]
  public string GetRemoteIpAddress(HttpContext context) {
    var remoteIpAddress = context.Connection.RemoteIpAddress;
    return remoteIpAddress?.ToString() ?? "No remote IP address";
  }


  [Endpoint("/EndpointWithManyRequestValues2")]
  public string EndpointWithManyRequestValues2(HttpContext context, int id, string name) {
    return "Id: " + id + " Name:" + name;
  }

  [Endpoint("/EndpointWithManyRequestValues")]
  public string EndpointWithManyRequestValues(HttpContext context, string id, int my) {
    return "Works";
  }
  
  [EndpointFilter(typeof(SampleEndpointFilter))]
  [Endpoint ("/WithEndpointFilter/{id}")]
  [Authorize(AuthenticationSchemes = "Discord")]
  public string Handle(HttpContext context, string id)
  {
    return $"Hello {id}!";
  }
}