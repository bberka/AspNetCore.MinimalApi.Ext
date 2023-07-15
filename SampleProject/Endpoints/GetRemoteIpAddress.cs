namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

public class GetRemoteIpAddress : BaseEndpointSync.WithoutRequest.WithResult<string>
{
  public override string Handle(HttpContext context) {
    var remoteIpAddress = context.Connection.RemoteIpAddress;
    return remoteIpAddress?.ToString() ?? "No remote IP address";
  }
}