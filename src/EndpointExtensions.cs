using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Ext;

public static class EndpointExtensions
{
  public static void AddMinimalApiEndpointOptions(this WebApplicationBuilder builder, Action<EndpointOptions> action) {
    var options = new EndpointOptions();
    action(options);
    EndpointOptions.Options = options;
    builder.Services.AddSingleton(options);
  }

  public static void AddMinimalApiEndpointOptions(this WebApplicationBuilder builder, EndpointOptions options) {
    EndpointOptions.Options = options;
    builder.Services.AddSingleton(options);
  }

  public static void UseMinimalApiEndpoints(this WebApplication app) {
    var results = Assembly.GetEntryAssembly().GetExportedTypeResults();
    if (results.Count == 0) return;
    foreach (var classResult in results) {
      var instance = ActivatorUtilities.CreateInstance(app.Services, classResult.Type);
      if (instance is not BaseEndpoint casted)
        continue;
      var methodDelegate = classResult.EndpointMethod.CreateDelegate(classResult.EndpointMethod.GetDelegateType(), instance);
      var apiPath = classResult.Type.CreateApiPathFromAssembly(classResult.EndpointAttribute);
      var call = classResult.EndpointAttribute.Method switch {
        HttpMethodType.Post => app.MapPost(apiPath, methodDelegate),
        HttpMethodType.Get => app.MapGet(apiPath, methodDelegate),
        HttpMethodType.Put => app.MapPut(apiPath, methodDelegate),
        HttpMethodType.Delete => app.MapDelete(apiPath, methodDelegate),
        HttpMethodType.Patch => app.MapPatch(apiPath, methodDelegate),
        _ => throw new ArgumentOutOfRangeException(nameof(HttpMethodType))
      };
      if (EndpointOptions.Options.AuthorizeData is not null) call.RequireAuthorization(EndpointOptions.Options.AuthorizeData);
      if (classResult.Authorize is not null) call.RequireAuthorization(classResult.Authorize);
      foreach (var filterItem in EndpointOptions.Options.EndpointFilters)
        if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
          call.AddEndpointFilter(filter);
      foreach (var filterItem in classResult.Filters)
        if (ActivatorUtilities.CreateInstance(app.Services, filterItem) is IEndpointFilter filter)
          call.AddEndpointFilter(filter);
    }
  }
}