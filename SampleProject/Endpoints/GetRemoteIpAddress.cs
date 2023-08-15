namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

[Endpoint]
public class GetRemoteIpAddress 
{

  public string Handle(HttpContext context)
  {
    var remoteIpAddress = context.Connection.RemoteIpAddress;
    return remoteIpAddress?.ToString() ?? "No remote IP address";
  }
}