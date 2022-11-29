namespace Selfrated.MinimalAPI.Middleware.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointAPIAttribute : Attribute
{
    public EndpointAPIAttribute()
    {
    }

    public string? UrlPrefix { get; set; } = "";
    public bool RequireAuthentication
    {
        get
        {
            return AuthenticationRequired == AuthenticationRequired.Yes;
        }
    }
    public AuthenticationRequired AuthenticationRequired { get; set; } = AuthenticationRequired.Inherit;
    public string[]? Roles { get; set; } = null;
}
