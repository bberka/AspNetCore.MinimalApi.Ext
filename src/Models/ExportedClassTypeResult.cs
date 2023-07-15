using System.Reflection;
using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext.Models;

internal class ExportedClassTypeResult
{
  public ExportedClassTypeResult(Type type) {
    Type = type;
    Route = type.GetCustomAttribute<EndpointRouteAttribute>();
    Authorize = type.GetCustomAttribute<EndpointAuthorizeAttribute>();
    Filters = type.GetCustomAttributes<EndpointFilterAttribute>().Select(x => x.Type).ToArray() ?? Array.Empty<Type>();
    HttpMethods = type.GetCustomAttribute<EndpointHttpMethodAttribute>()?.HttpMethods ?? new[] { HttpMethodTypes.GET };
  }

  public Type Type { get; set; }
  public EndpointRouteAttribute? Route { get; set; }
  public Type[] Filters { get; set; }
  public IAuthorizeData? Authorize { get; set; }
  public HttpMethodTypes[] HttpMethods { get; set; }
}