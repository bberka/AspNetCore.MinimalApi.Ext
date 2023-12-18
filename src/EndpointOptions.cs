namespace AspNetCore.MinimalApi.Ext;

public sealed class EndpointOptions
{
  public AuthorizeData? AuthorizeData { get; set; } = null;

  public Type[] EndpointFilters { get; set; } = Array.Empty<Type>();

  /// <summary>
  ///   Sets global prefix for all routes.
  /// </summary>
  public string? GlobalPrefix { get; set; } = "api/";
  
  internal bool UseGlobalPrefix => !string.IsNullOrWhiteSpace(GlobalPrefix) && GlobalPrefix != string.Empty;

  internal static EndpointOptions Options { get; set; } = Default;
  
  private static EndpointOptions Default => new();

}