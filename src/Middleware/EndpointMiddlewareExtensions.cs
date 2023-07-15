using System.Reflection;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Ext.Middleware;

public static class EndpointMiddlewareExtensions
{
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

  private static void UseEndpoints(this WebApplication app, EndpointMiddlewareOptions options) {
    //get all assemblies that have the ApiEndpointAttribute attribute
    var entryAssembly = Assembly.GetEntryAssembly();
    var mainName = entryAssembly?.GetName().Name ?? throw new ArgumentNullException(nameof(entryAssembly));
    var results = entryAssembly.GetExportedTypeResults();
    if (results.Count == 0) return;

    foreach (var classResult in results) {
      //instantiate the class
      var classInstance = Activator.CreateInstance(classResult.Type);

      if (classInstance == null)
        continue;

      //get methods that have the EndpointMethodAttribute
      var methods = classResult.Type.GetEndpointHandlerMethods();

      foreach (var method in methods) {
        var methodDelegate = method.Method.CreateDelegate(
          method.Method.GetDelegateType(),
          classInstance);

        var path = InternalUtils.GetUrlPath(options, classResult.Route, classResult.Type.Name,
          classResult.Type.GetContainingFolderName(), mainName);

        //var authData = options.GetAuthorizeData(classResult.Type, method.Method);
        //var filters = options.GetHttpFiltersByType(classResult.Type, method.Method);
        var globalFilters = options.EndpointFilters;

        foreach (var httpMethod in classResult.HttpMethods) {
          var call = httpMethod switch {
            HttpMethodTypes.POST => app.MapPost(path, methodDelegate),
            HttpMethodTypes.GET => app.MapGet(path, methodDelegate),
            HttpMethodTypes.PUT => app.MapPut(path, methodDelegate),
            HttpMethodTypes.DELETE => app.MapDelete(path, methodDelegate),
            HttpMethodTypes.PATCH => app.MapPatch(path, methodDelegate),
            _ => throw new ArgumentOutOfRangeException(nameof(httpMethod))
          };

          if (call == null) continue;
          if (options.AuthorizeData is not null) call.RequireAuthorization(options.AuthorizeData);

          if (classResult.Authorize is not null) call.RequireAuthorization(classResult.Authorize);

          foreach (var filterItem in classResult.Filters)
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);

          foreach (var filterItem in globalFilters)
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);
        }
      }
    }
  }
}


