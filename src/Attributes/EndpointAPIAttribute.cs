namespace Selfrated.MinimalAPI.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointAPIAttribute : Attribute
{

    public string Route { get; set; } = "";
    internal bool UseRoute => !string.IsNullOrWhiteSpace(Route) && Route != "";

}
