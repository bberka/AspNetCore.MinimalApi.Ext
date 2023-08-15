using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints.Product.Comment.Image;

[Endpoint(HttpMethodType.Post,CustomRoute = "product/comment/image")]
public sealed class GetProductCommentImageEndpoint : BaseEndpoint
{
  
  public string Handle(HttpContext context, [FromQuery] string message) => !string.IsNullOrEmpty(message) ? message : "No message provided";
}