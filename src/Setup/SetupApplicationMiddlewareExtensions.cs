using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.MinimalApi.Ext.Setup;

public static class SetupApplicationMiddlewareExtensions
{
  /// <summary>
  ///   This will enable any classes implementing IApplicationSetup
  /// </summary>
  /// <param name="app"></param>
  public static void UseApplicationSetup(this WebApplication app) {
    var results = Assembly.GetCallingAssembly()
                          .ExportedTypes
                          .Where(x => typeof(IApplicationSetup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                          .Select(Activator.CreateInstance)
                          .Cast<IApplicationSetup>()
                          .OrderBy(x => x.InitializationOrder);

    foreach (var result in results) result.InitializeApplication(app);
  }
}