using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.MinimalApi.Ext.Setup;

public static class SetupBuilderExtensions
{
  /// <summary>
  ///   This will enable any classes implementing IBuilderServiceSetup
  /// </summary>
  /// <param name="builder"></param>
  public static void UseBuilderSetup(this WebApplicationBuilder builder) {
    var results = Assembly.GetCallingAssembly()
                          .ExportedTypes
                          .Where(x => typeof(IBuilderServiceSetup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                          .Select(Activator.CreateInstance)
                          .Cast<IBuilderServiceSetup>();

    foreach (var result in results) result.InitializeServices(builder);
  }
}