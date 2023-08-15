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
  
  /// <summary>
  /// Removes "Endpoints" or "Endpoint" string from created route.
  /// <br/>
  /// This will not apply if Custom Route is set
  /// </summary>
  public bool RemoveEndpointStringInRoute { get; set; } = true;
  
  /// <summary>
  /// Endpoint method name. Default is "Handle"
  /// </summary>
  public string DefaultEndpointMethodName { get; set; } = "Handle";

  
  internal bool UseGlobalPrefix => !string.IsNullOrWhiteSpace(GlobalPrefix) && GlobalPrefix != string.Empty;


  internal static EndpointOptions Options { get; set; } = Default;
  
  private static EndpointOptions Default => new();

}