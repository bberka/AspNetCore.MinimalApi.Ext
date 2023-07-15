using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.MinimalApi.Ext.Middleware;

public static class SetupApplicationMiddlewareExtensions
{
  /// <summary>
  ///   This will enable any classes implementing IApplicationSetup
  /// </summary>
  /// <param name="app"></param>
  public static void UseApplicationSetup(this WebApplication app) {
    var results = Assembly.GetCallingAssembly().ExportedTypes
      .Where(x => typeof(IApplicationSetup).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
      .Select(Activator.CreateInstance).Cast<IApplicationSetup>();

    foreach (var result in results) result.InitializeApplication(app);
  }
}

public interface IApplicationSetup
{
  /// <summary>
  ///   This is where you can add any app setup
  /// </summary>
  /// <param name="app"></param>
  void InitializeApplication(WebApplication app);
}