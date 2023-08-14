using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

public sealed class EndpointHttpGet : EndpointMethodAttribute
{
  public EndpointHttpGet() : base(HttpMethodType.GET) { }
}