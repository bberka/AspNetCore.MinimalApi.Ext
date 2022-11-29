
namespace Selfrated.MinimalAPI.Middleware.Attributes;

[Flags]
public enum RouteType
{
    GET = 1,
    POST = 2,
    PUT = 4,
    DELETE = 8,
    Inherit = 16
}

public enum AuthenticationRequired
{
    Yes,
    No,
    Inherit
}

[AttributeUsage(AttributeTargets.Method)]
public class EndpointMethodAttribute : Attribute
{
    public string? Name { get; set; } = null;
    public AuthenticationRequired AuthenticationRequired { get; set; } = AuthenticationRequired.Inherit;
    
    public RouteType RouteType { get; set; } = RouteType.Inherit;

    public string? UrlPrefixOverride { get; set; } = null;
    
    public string[]? Roles { get; set; } = null;
    public Type[]? EndpointFilters { get; set; }

}
