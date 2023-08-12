namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class BasicEndpointExample : BaseEndpointSync.WithoutRequest.WithResult<string>
{
  public override string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}