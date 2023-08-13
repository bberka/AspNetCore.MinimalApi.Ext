using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

public sealed class EndpointHttpPut : EndpointMethodAttribute
{
  public EndpointHttpPut() : base(HttpMethodType.PUT)
  {
  }
}
