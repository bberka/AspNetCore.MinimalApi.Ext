namespace AspNetCore.MinimalApi.Ext.Sample.Classes;

public class AppSettings
{
  public List<string> CorsAllowedUrls { get; set; } = new();
}

public class SwaggerSettings
{
  public string ApplicationName { get; set; } = string.Empty;
  public string Version { get; set; } = string.Empty;
}

public class AzureAd
{
  public string? Instance { get; set; }
  public string? ClientId { get; set; }
  public string? TenantId { get; set; }
  public string? Scope { get; set; }
}