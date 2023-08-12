using System.Reflection;
using AspNetCore.MinimalApi.Ext.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Ext;

public static class EndpointExtensions
{
  public static void UseMinimalApiEndpoints(this WebApplication app, Action<EndpointOptions> action)
  {
    var options = new EndpointOptions();
    action(options);
    InternalUseEndpoints(app, options);
  }

  public static void UseMinimalApiEndpoints(this WebApplication app, EndpointOptions options)
  {
    InternalUseEndpoints(app, options);
  }

  public static void UseMinimalApiEndpoints(this WebApplication app)
  {
    var options = new EndpointOptions();
    InternalUseEndpoints(app, options);
  }

  private static void InternalUseEndpoints(this WebApplication app, EndpointOptions options)
  {
    var entryAssembly = Assembly.GetEntryAssembly();
    var mainName = entryAssembly?.GetName().Name ?? throw new ArgumentNullException(nameof(entryAssembly));
    var results = entryAssembly.GetExportedTypeResults();
    if (results.Count == 0) return;

    foreach (var classResult in results) {
      var classInstance = Activator.CreateInstance(classResult.Type);

      if (classInstance == null)
        continue;

      var methods = classResult.Type.GetEndpointHandlerMethods();

      foreach (var methodResult in methods) {
        var methodDelegate = methodResult.Method.CreateDelegate(methodResult.Method.GetDelegateType(), classInstance);

        var path = InternalUtils.GetUrlPath(options, classResult.Route, classResult.Type.Name,
          classResult.Type.GetContainingFolderName(), mainName);

        var globalFilters = options.EndpointFilters;

        foreach (var httpMethod in classResult.HttpMethods) {
          var call = httpMethod switch
          {
            HttpMethodTypes.POST => app.MapPost(path, methodDelegate),
            HttpMethodTypes.GET => app.MapGet(path, methodDelegate),
            HttpMethodTypes.PUT => app.MapPut(path, methodDelegate),
            HttpMethodTypes.DELETE => app.MapDelete(path, methodDelegate),
            HttpMethodTypes.PATCH => app.MapPatch(path, methodDelegate),
            _ => throw new ArgumentOutOfRangeException(nameof(httpMethod))
          };

          if (options.AuthorizeData is not null) call.RequireAuthorization(options.AuthorizeData);

          if (classResult.Authorize is not null) call.RequireAuthorization(classResult.Authorize);

          foreach (var filterItem in globalFilters)
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);
          foreach (var filterItem in classResult.Filters)
            if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
              call.AddEndpointFilter(filter);
        }
      }
    }
  }
}