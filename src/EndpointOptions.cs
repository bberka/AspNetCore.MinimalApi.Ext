namespace AspNetCore.MinimalApi.Ext;

public sealed class EndpointOptions
{
  //public bool AuthenticationRequired { get; set; } = false;
  public AuthorizeData? AuthorizeData { get; set; } = null;

  public Type[] EndpointFilters { get; set; } = Array.Empty<Type>();

  /// <summary>
  ///   Sets global prefix for all routes.
  /// </summary>
  public string? GlobalPrefix { get; set; } = "api";

  public bool RemoveEndpointsStringInFolderName { get; set; } = true;
  public bool RemoveEndpointStringInClassName { get; set; } = true;
  
  
  public const string DefaultEndpointMethodName = "Handle";

  
  internal bool UseGlobalPrefix => !string.IsNullOrWhiteSpace(GlobalPrefix) && GlobalPrefix != "";

  private static EndpointOptions Default => new();

  internal static EndpointOptions Options { get; set; } = Default;

}