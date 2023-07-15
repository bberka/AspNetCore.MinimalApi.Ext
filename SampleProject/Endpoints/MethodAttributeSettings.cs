namespace AspNetCore.MinimalApi.Ext.Sample.Endpoints;

//[EndpointAPI]
//public class MethodAttributeSettings
//{
//    [EndpointHttpMethodAttribute]
//    public string HelloWorld()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(RouteType = RouteType.GET | RouteType.POST)]
//    public string HelloWorldPostAndGet()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute]
//    public async Task<string> HelloWorldAsync()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(AuthenticationRequired = AuthenticationRequired.Yes)]
//    public string HelloWorldAuthRequired()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(Name = "CustomName")]
//    public string HelloWorldOverrideName()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(UrlPrefixOverride = "Overridden")]
//    public string HelloWorldOverridePathPrefix()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(UrlPrefixOverride = "")]
//    public string HelloWorldNoPathPrefix()
//    {
//        return "Hello World!";
//    }

//    [EndpointHttpMethodAttribute(Name = "CustomName/{id}")]
//    public string CustomNameWithUrlParams(string id)
//    {
//        return $"Hello {id}!";
//    }

//    [EndpointHttpMethodAttribute(Name = "Hi/There/Method/Addtribute/Settings/{id}", RouteType = RouteType.GET)]
//    public string LongPath(string id)
//    {
//        return $"Hello {id}!";
//    }
//}
