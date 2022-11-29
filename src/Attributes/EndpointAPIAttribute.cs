namespace Selfrated.MinimalAPI.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointAPIAttribute : Attribute
{
    public string? Name { get; set; } = "";
    public AuthenticationRequired AuthenticationRequired { get; set; } = AuthenticationRequired.Inherit;
    public RouteType RouteType { get; set; } = RouteType.Inherit;
    public string[]? Roles { get; set; } = null;
    public Type[]? EndpointFilters { get; set; }
}
