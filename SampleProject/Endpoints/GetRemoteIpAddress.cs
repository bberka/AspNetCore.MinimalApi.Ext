namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class GetRemoteIpAddress : BaseEndpoint
{
  public string Handle(HttpContext context)
  {
    var remoteIpAddress = context.Connection.RemoteIpAddress;
    return remoteIpAddress?.ToString() ?? "No remote IP address";
  }
}