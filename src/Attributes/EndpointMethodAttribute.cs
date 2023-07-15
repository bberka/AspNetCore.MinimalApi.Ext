using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointMethodAttribute : Attribute
{
  public EndpointMethodAttribute(HttpMethodTypes httpMethodTypes) {
    HttpMethods = new[] { httpMethodTypes };
  }

  public EndpointMethodAttribute(params HttpMethodTypes[] httpMethod) {
    HttpMethods = httpMethod;
  }

  public HttpMethodTypes[] HttpMethods { get; set; }
}
