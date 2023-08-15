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
    else EndpointAttribute = new EndpointAttribute();
  }
  public Type Type { get; }
  public Type[] Filters { get; }
  public IAuthorizeData? Authorize { get; }
  public EndpointAttribute EndpointAttribute { get; }
  public MethodInfo EndpointMethod { get; set; }
}