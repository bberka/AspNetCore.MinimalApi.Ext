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
    Endpoints = type.GetMethods()
                    .Where(x => x.GetCustomAttribute<EndpointAttribute>() != null && x is { IsPublic: true, IsStatic: false })
                    .Select(x => new ExportedMethodTypeResult(x))
                    .ToList();
  }
  public Type Type { get; }
  public Type[] Filters { get; }
  public IAuthorizeData? Authorize { get; }
  public List<ExportedMethodTypeResult> Endpoints { get; set; }
}