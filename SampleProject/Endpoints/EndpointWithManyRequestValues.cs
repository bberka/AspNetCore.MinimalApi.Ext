namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class EndpointWithManyRequestValues : BaseEndpoint
{
  public string Handle(HttpContext context, string id, int my)
  {
    return "Works";
  }
}

public class EndpointWithManyRequestValues2 : BaseEndpoint
{
  public string Handle(HttpContext context, int id, string name)
  {
    return "Id: " + id + " Name:" + name;
  }
}