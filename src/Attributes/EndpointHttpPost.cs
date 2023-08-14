using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

public sealed class EndpointHttpPost : EndpointMethodAttribute
{
  public EndpointHttpPost() : base(HttpMethodType.POST) { }
}