namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

//[EndpointAPI]
//public class MethodAttributeSettings
//{
//    [EndpointMethodAttribute]
//    public string HelloWorld()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(RouteType = RouteType.GET | RouteType.POST)]
//    public string HelloWorldPostAndGet()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute]
//    public async Task<string> HelloWorldAsync()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(AuthenticationRequired = AuthenticationRequired.Yes)]
//    public string HelloWorldAuthRequired()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(Name = "CustomName")]
//    public string HelloWorldOverrideName()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(UrlPrefixOverride = "Overridden")]
//    public string HelloWorldOverridePathPrefix()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(UrlPrefixOverride = "")]
//    public string HelloWorldNoPathPrefix()
//    {
//        return "Hello World!";
//    }

//    [EndpointMethodAttribute(Name = "CustomName/{id}")]
//    public string CustomNameWithUrlParams(string id)
//    {
//        return $"Hello {id}!";
//    }

//    [EndpointMethodAttribute(Name = "Hi/There/Method/Addtribute/Settings/{id}", RouteType = RouteType.GET)]
//    public string LongPath(string id)
//    {
//        return $"Hello {id}!";
//    }
//}
