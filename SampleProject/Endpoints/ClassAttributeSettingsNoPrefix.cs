namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class BasicEndpointExample : BaseEndpoint
{
  public string Handle(HttpContext context)
  {
    return "Hello World!";
  }
}