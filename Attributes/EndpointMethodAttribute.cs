
namespace Selfrated.MinimalAPI.Middleware.Attributes;

[Flags]
public enum MethodTypeEnum
{
    GET = 1,
    POST = 2,
    PUT = 4,
    DELETE = 8
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
    public bool? RequireAuthentication
    {
        get
        {
            if (AuthenticationRequired == AuthenticationRequired.Inherit)
                return null;

            return AuthenticationRequired == AuthenticationRequired.Yes;
        }
    }
    public AuthenticationRequired AuthenticationRequired { get; set; } = AuthenticationRequired.Inherit;
    public string[]? Roles { get; set; } = null;
    public MethodTypeEnum MethodType { get; set; } = MethodTypeEnum.POST;

    public string? PrefixOverride { get; set; } = null;

    public Type? EndpointFilter { get; set; }

}
