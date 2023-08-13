using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

public sealed class EndpointHttpPatch : EndpointMethodAttribute
{
  public EndpointHttpPatch() : base(HttpMethodType.PATCH)
  {
  }
}