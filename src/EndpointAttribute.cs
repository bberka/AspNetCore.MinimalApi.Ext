using Microsoft.AspNetCore.Http;

namespace AspNetCore.MinimalApi.Ext;

public sealed class EndpointAttribute : Attribute
{
  /// <summary>
  /// Determines the HTTP method for the endpoint class.
  /// </summary>
  /// <param name="methodType">Specifies http method for endpoint, default value is GET</param>
  public EndpointAttribute(HttpMethodType methodType = HttpMethodType.Get) {
    Method = methodType;
  }
  /// <summary>
  /// HttpMethod for endpoint
  /// </summary>
  public HttpMethodType Method { get;  init; } 
  
  /// <summary>
  /// Gets or sets the name of the action. If this value is set, auto action name will be overwritten. Default is class name.
  /// </summary>
  public string? ActionName { get; init; }
  
  /// <summary>
  /// Specifies custom route for endpoint, global prefix still apply. If this value is set, action name value will be ignored.
  /// </summary>
  public string? CustomRoute { get; init; }
}