using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Ext;

public static class EndpointExtensions
{
  /// <summary>
  ///   Adds the minimal API endpoint options to the service collection as singleton and for library to use.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="action"></param>
  public static IServiceCollection AddMinimalApiEndpointOptions(this IServiceCollection services, Action<EndpointOptions> action) {
    var options = new EndpointOptions();
    action(options);
    EndpointOptions.Options = options;
    services.AddSingleton(options);
    return services;
  }

  /// <summary>
  /// Adds the minimal API endpoint options to the service collection as singleton and for library to use.
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="options"></param>
  public static void AddMinimalApiEndpointOptions(this WebApplicationBuilder builder, EndpointOptions options) {
    EndpointOptions.Options = options;
    builder.Services.AddSingleton(options);
  }

  /// <summary>
  /// Initializes the minimal API endpoints from the entry assembly.
  /// </summary>
  /// <param name="app"></param>
  /// <exception cref="NullReferenceException"></exception>
  public static void UseMinimalApiEndpoints(this WebApplication app) {
    app.UseMinimalApiEndpoints(Assembly.GetEntryAssembly() ?? throw new NullReferenceException("Entry assembly is null"));
  }

  /// <summary>
  /// Initializes the minimal API endpoints from the specified assembly.
  /// </summary>
  /// <param name="app"></param>
  /// <param name="assembly"></param>
  /// <exception cref="ArgumentOutOfRangeException"></exception>
  public static void UseMinimalApiEndpoints(this WebApplication app, Assembly assembly) {
    var results = assembly.GetExportedTypeResults();
    if (results.Count == 0) return;
    foreach (var classResult in results) {
      var instance = ActivatorUtilities.CreateInstance(app.Services, classResult.Type);
      if (instance is null)
        continue;
      foreach (var method in classResult.Endpoints) {
        var methodDelegate = method.MethodInfo.CreateDelegate(method.MethodInfo.GetDelegateType(), instance);
        if (method.Endpoint is null) continue;
        var apiPath = EndpointOptions.Options.UseGlobalPrefix
                        ? EndpointOptions.Options.GlobalPrefix + method.Endpoint.Route
                        : method.Endpoint.Route;
        apiPath = apiPath.Replace("//", "/");
        var call = method.Endpoint.Method switch {
          HttpMethodType.Post => app.MapPost(apiPath, methodDelegate),
          HttpMethodType.Get => app.MapGet(apiPath, methodDelegate),
          HttpMethodType.Put => app.MapPut(apiPath, methodDelegate),
          HttpMethodType.Delete => app.MapDelete(apiPath, methodDelegate),
          HttpMethodType.Patch => app.MapPatch(apiPath, methodDelegate),
          _ => throw new ArgumentOutOfRangeException(nameof(HttpMethodType))
        };
        if (EndpointOptions.Options.AuthorizeData is not null)
          call.RequireAuthorization(EndpointOptions.Options.AuthorizeData);
        if (classResult.Authorize is not null)
          call.RequireAuthorization(classResult.Authorize);
        if (method.Authorize is not null)
          call.RequireAuthorization(method.Authorize);
        foreach (var filterItem in EndpointOptions.Options.EndpointFilters)
          if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
            call.AddEndpointFilter(filter);
        foreach (var filterItem in classResult.Filters)
          if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
            call.AddEndpointFilter(filter);
        foreach (var filterItem in method.Filters)
          if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
            call.AddEndpointFilter(filter);
      }
    }
  }
}