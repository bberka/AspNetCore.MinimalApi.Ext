using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product.Comment;

[Endpoint(HttpMethodType.Delete)]
public sealed class Remove 
{

  public bool Handle(HttpContext context, [FromQuery] string message) => !string.IsNullOrEmpty(message);
}