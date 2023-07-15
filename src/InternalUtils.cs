using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AspNetCore.MinimalApi.Ext;

internal static class InternalUtils
{
  private const string DefaultEndpointMethodName = "Handle";

  internal static string GetUrlPath(EndpointMiddlewareOptions options, EndpointRouteAttribute? routeAttribute,
    string className, string controllerName, string entryAssemblyMainName) {
    var sb = new StringBuilder();
    sb.Append("/");
    if (options.UseGlobalPrefix) {
      sb.Append(options.GlobalPrefix);
      sb.Append("/");
    }

    if (routeAttribute is not null) {
      sb.Append(routeAttribute.Template);
      sb.Append("/");
      return FixUrl(sb).ToString();
    }

    var canUse = !controllerName.Equals("Endpoints", StringComparison.OrdinalIgnoreCase) &&
                 !controllerName.Equals(entryAssemblyMainName, StringComparison.OrdinalIgnoreCase);
    if (!canUse) {
      sb.Append(className);
      sb.Append("/");
      return FixUrl(sb).ToString();
    }

    sb.Append(controllerName);
    sb.Append("/");
    sb.Append(className);
    sb.Append("/");
    return FixUrl(sb).ToString();

    StringBuilder FixUrl(StringBuilder sb) {
      return sb.Replace("//", "/");
    }
  }

  internal static Type GetDelegateType(this MethodInfo method) {
    var args = new List<Type>(
      method.GetParameters().Select(p => p.ParameterType));

    Type delegateType;

    if (method.ReturnType == typeof(void)) {
      delegateType = Expression.GetActionType(args.ToArray());
    }
    else {
      args.Add(method.ReturnType);
      delegateType = Expression.GetFuncType(args.ToArray());
    }

    return delegateType;
  }


  internal static string GetContainingFolderName(this Type type) {
    var name = type.Namespace;
    var split = name?.Split('.');
    return split?[^1] ?? "";
  }

  internal static List<ExportedClassTypeResult> GetExportedTypeResults(this Assembly entryAssembly) {
    var results = entryAssembly?.ExportedTypes
      .Select(itemType => new ExportedClassTypeResult(itemType))
      .ToList();
    return results ?? new List<ExportedClassTypeResult>();
  }

  internal static List<ExportedMethodResult> GetEndpointHandlerMethods(this Type classType) {
    var methods = classType.GetMethods()
      .Where(x => x is { IsPublic: true, Name: DefaultEndpointMethodName, IsStatic: false })
      .Select(method => new ExportedMethodResult(method))
      .ToList();
    return methods;
  }
}