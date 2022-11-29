using Selfrated.MinimalAPI.Middleware.Attributes;

namespace SampleProject.Endpoints;

[EndpointAPI]
public class MethodAttributeSettings
{
    [EndpointMethod]
    public string HelloWorld()
    {
        return "Hello World!";
    }

    [EndpointMethod(RouteType = RouteType.GET | RouteType.POST)]
    public string HelloWorldPostAndGet()
    {
        return "Hello World!";
    }

    [EndpointMethod]
    public async Task<string> HelloWorldAsync()
    {
        return "Hello World!";
    }

    [EndpointMethod(AuthenticationRequired = AuthenticationRequired.Yes)]
    public string HelloWorldAuthRequired()
    {
        return "Hello World!";
    }

    [EndpointMethod(Name = "CustomName")]
    public string HelloWorldOverrideName()
    {
        return "Hello World!";
    }

    [EndpointMethod(UrlPrefixOverride = "Overridden")]
    public string HelloWorldOverridePathPrefix()
    {
        return "Hello World!";
    }

    [EndpointMethod(UrlPrefixOverride = "")]
    public string HelloWorldNoPathPrefix()
    {
        return "Hello World!";
    }
}
