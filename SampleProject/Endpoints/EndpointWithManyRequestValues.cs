namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class EndpointWithManyRequestValues : BaseEndpoint
{
  public string Handle(HttpContext context, string id, int my) {
    return "Works";
  }
}