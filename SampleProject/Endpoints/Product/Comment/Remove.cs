using AspNetCore.MinimalApi.Ext.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product.Comment;

[EndpointHttpDelete]
[EndpointRoute(typeof(Remove))]
public class Remove : BaseEndpoint
{

  public bool Handle(HttpContext context, [FromQuery] string message)
  {
    if (string.IsNullOrEmpty(message)) return false;
    return true;
  }
}