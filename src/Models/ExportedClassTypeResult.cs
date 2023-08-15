using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext.Models;

internal sealed class ExportedClassTypeResult
{
  public ExportedClassTypeResult(Type type) {
    Type = type;
    Authorize = type.GetCustomAttribute<AuthorizeAttribute>();
    var allowAnonymous = type.GetCustomAttribute<AllowAnonymousAttribute>();
    if (allowAnonymous is not null) Authorize = null;
    Filters = type.GetCustomAttributes<EndpointFilterAttribute>().Select(x => x.Type).ToArray();
    var endpoint = type.GetCustomAttribute<EndpointAttribute>();
    if (endpoint is not null)  EndpointAttribute = endpoint;
    
  }
  public Type Type { get; set; }
  public Type[] Filters { get; set; }
  public IAuthorizeData? Authorize { get; set; }
  public EndpointAttribute EndpointAttribute { get; set; } = new();
  public MethodInfo EndpointMethod { get; set; }
}