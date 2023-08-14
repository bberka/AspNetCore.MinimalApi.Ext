using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCore.MinimalApi.Ext;

public static class MinimalApiSwaggerExtensions
{
  public static SwaggerGenOptions ConfigureMinimalApiTags(this SwaggerGenOptions options, string defaultTag = "Endpoints") {
    options.TagActionsBy(api => {
      if (api.GroupName != null) return new[] { api.GroupName };
      var relativePath = api.RelativePath;
      if (!string.IsNullOrEmpty(EndpointOptions.Options.GlobalPrefix)) relativePath = relativePath?.Replace(EndpointOptions.Options.GlobalPrefix, "");

      relativePath = relativePath?.Trim('/');
      if (string.IsNullOrEmpty(relativePath)) {
        var controllerName = api.ActionDescriptor.DisplayName;
        return new[] { $"{controllerName}" };
        // throw new InvalidOperationException("Unable to determine tag for endpoint.");
      }

      var pathSplit = relativePath.Split('/');
      pathSplit = pathSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
      var removedLastOne = pathSplit.Take(pathSplit.Length - 1);
      pathSplit = removedLastOne.ToArray();
      if (pathSplit.Length == 0) return new[] { $"{defaultTag}" };
      var mergedControllerName = string.Join("_", pathSplit);
      return new[] { $"{mergedControllerName}" };
    });
    return options;
  }
}