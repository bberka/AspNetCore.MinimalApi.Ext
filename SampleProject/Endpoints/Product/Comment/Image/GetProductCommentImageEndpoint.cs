using AspNetCore.MinimalApi.Ext.Sample.Classes;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product.Comment.Image;

[Endpoint(HttpMethodType.Post ,ActionName = "GetImage")]
[EndpointFilter(typeof(SampleEndpointFilter))]
public sealed class GetProductCommentImageEndpoint
{
  public string Handle(HttpContext context, [FromQuery] string message) => !string.IsNullOrEmpty(message) ? message : "No message provided";
}