using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Route("NameOverride")]
public class ClassAttributeSettings : BaseEndpointSync.WithoutRequest.WithResult<string>
{
  [HttpPost]
  public override string Handle(HttpContext context) {
    return "Hello World!";
  }
}
