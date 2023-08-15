namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class EndpointWithManyRequestValues
{

  public string Handle(HttpContext context, string id, int my)
  {
    return "Works";
  }
}
[Endpoint]
public class EndpointWithManyRequestValues2
{

  public string Handle(HttpContext context, int id, string name)
  {
    return "Id: " + id + " Name:" + name;
  }
}