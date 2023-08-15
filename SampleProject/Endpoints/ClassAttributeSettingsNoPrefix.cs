namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class BasicEndpointExample : BaseEndpoint
{

  public string Handle(HttpContext context) => "Hello World!";
}