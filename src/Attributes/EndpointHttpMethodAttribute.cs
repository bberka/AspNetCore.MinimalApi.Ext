using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointHttpMethodAttribute : Attribute
{
  public EndpointHttpMethodAttribute(HttpMethodTypes httpMethodTypes) {
    HttpMethods = new[] { httpMethodTypes };
  }

  public EndpointHttpMethodAttribute(params HttpMethodTypes[] httpMethod) {
    HttpMethods = httpMethod;
  }

  public HttpMethodTypes[] HttpMethods { get; set; }
}
