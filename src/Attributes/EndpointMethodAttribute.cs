using AspNetCore.MinimalApi.Ext.Enums;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointMethodAttribute : Attribute
{
  public EndpointMethodAttribute(HttpMethodType httpMethodType) {
    HttpMethods = new[] { httpMethodType };
  }

  public EndpointMethodAttribute(HttpMethodType httpMethodType1, params HttpMethodType[] httpMethod) {
    var httpMethodList = new List<HttpMethodType> { httpMethodType1 };
    httpMethodList.AddRange(httpMethod);
    HttpMethods = httpMethodList;
  }

  public IEnumerable<HttpMethodType> HttpMethods { get; }
}