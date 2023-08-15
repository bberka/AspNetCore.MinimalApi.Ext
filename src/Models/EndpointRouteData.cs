namespace AspNetCore.MinimalApi.Ext.Models;

internal sealed class EndpointRouteData 
{
  public HttpMethod Method { get; set; } = HttpMethod.Get;
  public string? ActionName { get; set; }
  public string? CustomRoute { get; set; }
}