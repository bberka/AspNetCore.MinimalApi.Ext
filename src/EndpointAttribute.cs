using Microsoft.AspNetCore.Http;

namespace AspNetCore.MinimalApi.Ext;

[AttributeUsage(AttributeTargets.Method , AllowMultiple = false, Inherited = false)]
public sealed class EndpointAttribute : Attribute
{
  /// <summary>
  /// Determines the HTTP method for the endpoint class.
  /// </summary>
  /// <param name="route"></param>
  /// <param name="methodType">Specifies http method for endpoint, default value is GET</param>
  public EndpointAttribute(string route,HttpMethodType methodType = HttpMethodType.Get) {
    Method = methodType;
    Route = route;
  }
  /// <summary>
  /// HttpMethod for endpoint
  /// </summary>
  public HttpMethodType Method { get;  } 
  
  
  /// <summary>
  /// Specifies custom route for endpoint, global prefix still apply. If this value is set, action name value will be ignored.
  /// </summary>
  public string Route { get; }
}