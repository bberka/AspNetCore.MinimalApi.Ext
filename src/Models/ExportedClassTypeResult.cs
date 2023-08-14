using System.Reflection;
using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Models;

internal sealed class ExportedClassTypeResult
{
  public ExportedClassTypeResult(Type type) {
    Type = type;
    Route = type.GetCustomAttribute<EndpointRouteAttribute>();
    if (Route is null) {
      var routeAttributes = type.GetCustomAttributes<RouteAttribute>();
      var routeAttribute = routeAttributes.FirstOrDefault();
      if (routeAttribute is not null) Route = new EndpointRouteAttribute(routeAttribute);
    }

    Authorize = type.GetCustomAttribute<AuthorizeAttribute>();
    var allowAnonymous = type.GetCustomAttribute<AllowAnonymousAttribute>();
    if (allowAnonymous is not null) Authorize = null;

    Filters = type.GetCustomAttributes<EndpointFilterAttribute>().Select(x => x.Type).ToArray();
    HttpMethods = type.GetHttpMethods();
  }

  public Type Type { get; set; }
  public EndpointRouteAttribute? Route { get; set; }
  public Type[] Filters { get; set; }
  public IAuthorizeData? Authorize { get; set; }
  public HttpMethodType[] HttpMethods { get; set; }
}