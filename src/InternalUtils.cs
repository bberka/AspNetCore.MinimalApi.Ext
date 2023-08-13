using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AspNetCore.MinimalApi.Ext.Attributes;
using AspNetCore.MinimalApi.Ext.Enums;
using AspNetCore.MinimalApi.Ext.Models;

namespace AspNetCore.MinimalApi.Ext;

internal static class InternalUtils
{
  
  private const string DefaultEndpointMethodName = "Handle";

  internal static string GetUrlPath(EndpointOptions options, EndpointRouteAttribute? routeAttribute,
    string className, string controllerName, string entryAssemblyMainName)
  {
    var sb = new StringBuilder();
    sb.Append("/");
    if (options.UseGlobalPrefix) {
      sb.Append(options.GlobalPrefix);
      sb.Append('/');
    }

    if (routeAttribute is not null) {
      sb.Append(routeAttribute.Template);
      sb.Append('/');
      return FixUrl(sb).ToString();
    }

    var canUse = !controllerName.Equals("Endpoints", StringComparison.OrdinalIgnoreCase) &&
                 !controllerName.Equals(entryAssemblyMainName, StringComparison.OrdinalIgnoreCase);
    if (!canUse) {
      sb.Append(className);
      sb.Append('/');
      return FixUrl(sb).ToString();
    }

    sb.Append(controllerName);
    sb.Append("/");
    sb.Append(className);
    sb.Append("/");
    return FixUrl(sb).ToString();

    StringBuilder FixUrl(StringBuilder sb2)
    {
      return sb2.Replace("//", "/");
    }
  }

  internal static Type GetDelegateType(this MethodInfo method)
  {
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


  internal static string GetContainingFolderName(this Type type)
  {
    var name = type.Namespace;
    var split = name?.Split('.');
    return split?[^1] ?? "";
  }

  internal static List<ExportedClassTypeResult> GetExportedTypeResults(this Assembly? entryAssembly)
  {
    var results = entryAssembly?.ExportedTypes
      .Select(itemType => new ExportedClassTypeResult(itemType))
      .ToList();
    return results ?? new List<ExportedClassTypeResult>();
  }

  internal static List<ExportedMethodResult> GetEndpointHandlerMethods(this Type classType)
  {
    var methods = classType.GetMethods()
      .Where(x => x is { IsPublic: true, Name: DefaultEndpointMethodName, IsStatic: false })
      .Select(method => new ExportedMethodResult(method))
      .ToList();
    return methods;
  }
  internal static HttpMethodType[] GetHttpMethods(this Type type)
  {
    var methods = type.GetCustomAttribute<EndpointMethodAttribute>()?.HttpMethods;
    return methods?.ToArray() ?? new[] { HttpMethodType.GET };

    // var methods = type.GetCustomAttribute<EndpointHttpMethodAttribute>()?.HttpMethods;
    // if (methods is null || methods.Length == 0) {
    //   var attributes = type.GetCustomAttribute<HttpMethodAttribute>();
    //   if (attributes is not null) {
    //     HttpMethods = new[] { HttpMethodTypes.GET };
    //   }
    //   else {
    //     var httpMethods = new List<HttpMethodTypes>();
    //     foreach (var item in attributes.HttpMethods) {
    //       var method = item.ToUpperInvariant();
    //       if (Enum.TryParse<HttpMethodTypes>(method, out var httpMethod)) {
    //         httpMethods.Add(httpMethod);
    //       }
    //     }
    //
    //     HttpMethods = httpMethods.ToArray();
    //   }
    // }
    //
    // if (HttpMethods is null || HttpMethods.Length == 0)
    //   HttpMethods = new[] { HttpMethodTypes.GET };
  }
}