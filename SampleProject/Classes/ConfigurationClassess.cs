namespace SampleProject.Classes;

public class AppSettings
{
    public List<string> CORSAllowedURLS { get; set; } = new List<string>();
}

public class SwaggerSettings
{
    public string ApplicationName { get; set; }
    public string Version { get; set; }

}

public class AzureAd
{
    public string? Instance { get; set; }
    public string? ClientId { get; set; }
    public string? TenantId { get; set; }
    public string? Scope { get; set; }
}