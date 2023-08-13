using Microsoft.AspNetCore.Builder;

namespace AspNetCore.MinimalApi.Ext.Setup;

public interface IApplicationSetup
{
  public int InitializationOrder { get;  }

  /// <summary>
  ///   This is where you can add any app setup
  /// </summary>
  /// <param name="app"></param>
  void InitializeApplication(WebApplication app);
}