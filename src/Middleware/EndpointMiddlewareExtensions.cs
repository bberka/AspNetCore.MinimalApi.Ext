using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Selfrated.MinimalAPI.Middleware.Attributes;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Selfrated.Middleware.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Xml.Linq;
using Selfrated.Middleware;

namespace Selfrated.MinimalAPI.Middleware;

public static class EndpointMiddlewareExtensions
{
  public class EndpointMiddlewareOptions
  {
    //public bool AuthenticationRequired { get; set; } = false;
    public AuthorizeData? AuthorizeData { get; set; } = null;

    public Type[] EndpointFilters { get; set; } = Array.Empty<Type>();

    /// <summary>
    /// Sets global prefix for all routes.
    /// </summary>
    public string? GlobalPrefix { get; set; } = null;
    internal bool UseGlobalPrefix => !string.IsNullOrWhiteSpace(GlobalPrefix) && GlobalPrefix != "";


  }
  public static void UseEndpointsAPIAttributes(this WebApplication app, Action<EndpointMiddlewareOptions> action) {
    var options = new EndpointMiddlewareOptions();
    action(options);
    UseEndpoints(app, options);
  }
  public static void UseEndpointsAPIAttributes(this WebApplication app, EndpointMiddlewareOptions options) {
    UseEndpoints(app, options);
  }
  public static void UseEndpointsAPIAttributes(this WebApplication app) {
    var options = new EndpointMiddlewareOptions();
    UseEndpoints(app, options);
  }
  private static string GetContainingFolderName(Type type) {
    var name = type.Namespace;
    //var name = assembly.;
    var split = name.Split('.');
    return split[split.Length - 1];
   
  }
  private static void UseEndpoints(this WebApplication app, EndpointMiddlewareOptions options) {
    //get all assemblies that have the EndpointAPIAttribute attribute
    var entryAssembly = Assembly.GetEntryAssembly();
    var mainName = entryAssembly?.GetName().Name ?? throw new ArgumentNullException(nameof(entryAssembly));
    var results = entryAssembly?.ExportedTypes
      .Select(itemType => new ExportedTypeResult() {
        Type = itemType,
        Attribute = itemType.GetCustomAttributes<EndpointAPIAttribute>(false).FirstOrDefault(),
      })
      .Where(e => e.Attribute != null && e.Type != null && e.Type is not null)
      .ToList();
    if (results is null) return;

    foreach (var classType in results) {
      //instantiate the class
      var classInstance = Activator.CreateInstance(classType.Type);

      if (classInstance == null)
        continue;

      //get methods that have the EndpointMethodAttribute
      var methods = classType.Type.GetMethods()
        .Where(x => x.IsPublic && x.Name == "Handle")
        .Select(method => new ExportedMethodResult() {
        Method = method,
        Attribute = method.GetCustomAttribute<EndpointMethodAttribute>()
      })
        .ToList();

      foreach (var method in methods) {
        var methodDelegate = method.Method.CreateDelegate(
                                GetDelegateType(method.Method),
                                classInstance);

        var path = GetUrlPath(options, classType.Attribute, classType.Type.Name, GetContainingFolderName(classType.Type), mainName);

        var methodTypes = GetHttpMethodTypes(options, method.Method);

        foreach (var httpMethod in methodTypes) {
          var call = (httpMethod) switch {
            "POST" => app.MapPost(path, methodDelegate),
            "GET" => app.MapGet(path, methodDelegate),
            "PUT" => app.MapPut(path, methodDelegate),
            "DELETE" => app.MapDelete(path, methodDelegate),
            _ => null,
          };

          if (call == null) continue;
          if (options.AuthorizeData is not null) {
            call.RequireAuthorization(options.AuthorizeData);
          }

          var authData = GetAuthorizeData(options, classType.Type, method.Method);
          if (authData is not null) {
            call.RequireAuthorization(authData);
          }

          var filters = GetHttpFiltersByType(options, classType.Type, method.Method);
          foreach (var filterItem in filters) {
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);
          }

          var globalFilters = options.EndpointFilters;
          foreach (var filterItem in globalFilters) {
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);
          }
        }
      }

    }
  }
  private static Type[] GetHttpFiltersByType(EndpointMiddlewareOptions options, Type classType, MethodInfo method) {
    var methodAttributes = method.CustomAttributes;

    var filterTypes = methodAttributes
      .Select(attributeData => attributeData.AttributeType)
      .Where(attributeType => typeof(Attribute).IsAssignableFrom(attributeType) && typeof(IEndpointFilter).IsAssignableFrom(attributeType))
      .ToList();

    var classAttributes = classType.CustomAttributes;

    filterTypes.AddRange(classAttributes
        .Select(attributeData => attributeData.AttributeType)
        .Where(attributeType => typeof(Attribute).IsAssignableFrom(attributeType) && typeof(IEndpointFilter).IsAssignableFrom(attributeType)));

    return filterTypes.ToArray() ?? Array.Empty<Type>();
  }

  //private static Type[]? GetEndpointFilters(EndpointMiddlewareOptions options, EndpointAPIAttribute? classAttribute, EndpointMethodAttribute? methodAttribute) {
  //  if (methodAttribute?.EndpointFilters != null)
  //    return methodAttribute?.EndpointFilters;

  //  if (classAttribute?.EndpointFilters != null)
  //    return classAttribute?.EndpointFilters;

  //  return options.EndpointFilters;
  //}



  private static string[] GetHttpMethodTypes(EndpointMiddlewareOptions options, MethodInfo method) {
    var methods = method.CustomAttributes
      .Where(x => x.AttributeType.BaseType == typeof(HttpMethodAttribute))
      .Select(x => GetHttpMethodStringFromAttributeName(x.AttributeType))
      .ToArray();
    if (methods.Length == 0) return new[] { "GET" };
    return methods;

    static string GetHttpMethodStringFromAttributeName(Type? type) {
      if (typeof(HttpGetAttribute) == type) return "GET";
      if (typeof(HttpPostAttribute) == type) return "POST";
      if (typeof(HttpPutAttribute) == type) return "PUT";
      if (typeof(HttpDeleteAttribute) == type) return "DELETE";
      return "GET";
    }
  }

  private static IAuthorizeData? GetAuthorizeData(EndpointMiddlewareOptions options, Type classType, MethodInfo methodType) {
    return methodType.GetCustomAttributes<AuthorizeAttribute>().FirstOrDefault()
      ?? classType.GetCustomAttributes<AuthorizeAttribute>().FirstOrDefault();
  
  }

  private static string GetUrlPath(EndpointMiddlewareOptions options, EndpointAPIAttribute classAttribute, string className, string controllerName, string entryAssemblyMainName) {
    var sb = new StringBuilder();
    sb.Append("/");
    if (options.UseGlobalPrefix) {
      sb.Append(options.GlobalPrefix);
      sb.Append("/");
    }

    if (classAttribute.UseRoute) {
      sb.Append(classAttribute.Route);
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

  private static Type GetDelegateType(MethodInfo method) {
    List<Type> args = new List<Type>(
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
}


