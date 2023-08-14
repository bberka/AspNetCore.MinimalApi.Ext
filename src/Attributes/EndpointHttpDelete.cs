using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

public sealed class EndpointHttpDelete : EndpointMethodAttribute
{
  public EndpointHttpDelete() : base(HttpMethodType.DELETE) { }
}