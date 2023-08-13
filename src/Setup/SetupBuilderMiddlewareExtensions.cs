using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Ext.Setup;

public static class SetupBuilderMiddlewareExtensions
{
  /// <summary>
  ///   This will enable any classes implementing IBuilderServiceSetup
  /// </summary>
  /// <param name="builder"></param>
  public static void UseBuilderSetup(this WebApplicationBuilder builder)
  {
    var results = Assembly.GetCallingAssembly()
      .ExportedTypes
      .Where(x => typeof(IBuilderServiceSetup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
      .Select(Activator.CreateInstance)
      .Cast<IBuilderServiceSetup>();

    foreach (var result in results) result.InitializeServices(builder);
  }
}

