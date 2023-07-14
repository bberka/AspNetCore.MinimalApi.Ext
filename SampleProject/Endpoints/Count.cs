using Microsoft.AspNetCore.Mvc;
using Selfrated.Middleware;
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace SampleProject.Endpoints;

[EndpointAPI]
public class GetProductCount : BaseEndpointSync
  .WithoutRequest
  .WithResult<int>
{
  public override int Handle() {
    return new Random().Next();
  }
}