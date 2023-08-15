namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class BasicEndpointExample
{

  public string Handle(HttpContext context) => "Hello World!";
}