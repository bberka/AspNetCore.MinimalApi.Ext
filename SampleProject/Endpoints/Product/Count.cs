using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Selfrated.Middleware;
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace SampleProject.Endpoints.Product;

[EndpointAPI]
[AllowAnonymous]

public class Count : BaseEndpointSync
  .WithoutRequest
  .WithResult<int>
{
  [HttpGet]
  public override int Handle() {
    return new Random().Next();
  }
}