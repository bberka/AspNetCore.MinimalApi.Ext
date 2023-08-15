using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AspNetCore.MinimalApi.Ext.Exceptions;
using AspNetCore.MinimalApi.Ext.Models;

namespace AspNetCore.MinimalApi.Ext;

internal static class InternalUtils
{
  internal static Type GetDelegateType(this MethodInfo method) {
    var args = new List<Type>(method.GetParameters().Select(p => p.ParameterType));

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


  internal static List<ExportedClassTypeResult> GetExportedTypeResults(this Assembly assembly) {
    var results = assembly.ExportedTypes
                               .Where(x => x.IsClass
                                           && x.GetCustomAttribute<EndpointAttribute>() is not null
                                           && !x.IsAbstract
                                           && x.IsPublic
                                           && x.GetMethods()
                                               .Any(y => y.Name == EndpointOptions.Options.DefaultEndpointMethodName && y.IsPublic && !y.IsStatic))
                               .Select(itemType => new ExportedClassTypeResult(itemType))
                               .ToList();
    foreach (var classResult in results) {
      var constructor = classResult.Type.GetConstructors();
      var anyPublicCtor = constructor.Any(x => x.IsPublic);
      if (!anyPublicCtor) throw new MustHavePublicConstructorException(classResult.Type);
      var anyEmptyCtor = constructor.Any(x => x.GetParameters().Length == 0);
      if (!anyEmptyCtor) throw new MustHaveParameterlessConstructorException(classResult.Type);
      var endpointMethod = classResult.Type.GetMethods().FirstOrDefault(x => x.Name == EndpointOptions.Options.DefaultEndpointMethodName);
      if (endpointMethod is null) throw new MustHaveEndpointMethodException(classResult.Type);
      var isEndpointMethodPublic = endpointMethod.IsPublic;
      if (!isEndpointMethodPublic) throw new MustHaveEndpointMethodPublicException(classResult.Type);
      classResult.EndpointMethod = endpointMethod;
    }
    return results;
  }

  internal static string CreateApiPathFromAssembly(this Type type, EndpointAttribute endpointAttribute) {
    var sb = new StringBuilder();
    sb.Append('/');
    if (EndpointOptions.Options.UseGlobalPrefix) {
      sb.Append(EndpointOptions.Options.GlobalPrefix);
      sb.Append('/');
    }

    if (!string.IsNullOrEmpty(endpointAttribute.CustomRoute)) {
      var customRoute = endpointAttribute.CustomRoute;
      if (EndpointOptions.Options.RemoveEndpointStringInRoute) customRoute = customRoute.RemoveEndpointText();
      sb.Append(customRoute);
      return sb.Replace("//", "/").ToString();
    }

    var entryAssembly = Assembly.GetEntryAssembly();
    if (entryAssembly == null || entryAssembly is null) throw new InvalidOperationException("Entry assembly not found");
    var baseAssemblyNameSpace = entryAssembly.GetName().Name;
    if (baseAssemblyNameSpace is null) throw new InvalidOperationException("Entry assembly name not found");
    var className = string.IsNullOrEmpty(endpointAttribute.ActionName)
                      ? type.Name
                      : endpointAttribute.ActionName;
    var cleanNameSpace = type.Namespace?.Replace(baseAssemblyNameSpace, "")?.Trim('.');
    var split = cleanNameSpace?.Split('.');

    if (EndpointOptions.Options.RemoveEndpointStringInRoute) className = className.RemoveEndpointText();
    if (split == null || split.Length == 0) {
      sb.Append(className);
      return sb.Replace("//", "/").ToString();
    }

    var joined = string.Join("/", split);
    if (EndpointOptions.Options.RemoveEndpointStringInRoute) joined = joined.RemoveEndpointText();
    sb.Append(joined);
    sb.Append('/');
    sb.Append(className);
    return sb.Replace("//", "/").ToString();
  }
  private static string RemoveEndpointText(this string text) => text.Replace("Endpoints", "").Replace("Endpoint", "");
}