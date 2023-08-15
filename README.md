# Minimal API Middleware

A set of tools to simplify creating AspNetCore applications, specifically when using MinimalAPIs.

The middleware helps clean up your code by making it easy to break the application startup into seperate classes,
ideally named by what their purpose is.

This project is forked and a lot of changes has been made.
[Click to go original repository](https://github.com/edelyn/Selfrated.MinimalAPI.Middleware)
## Requirements

AspNetCore applications, with at least .NET 7.0

## Installation Instructions

Install the package from NuGet:

```bash
Install-Package AspNetCore.MinimalApi.Ext
```

Install the package from CLI:
```bash
dotnet add package AspNetCore.MinimalApi.Ext
```

## Things to know before using
- Library only supports .NET 7.0 and above.
- You can not use classes with multiple endpoints. Each class must have single method that handles the endpoint. The default is method named "Handle" however you can change this by overriding EndpointOptions.
- Currently only class level custom attributes are supported.
- Model binding to query is not supported however you can use FromQueryAttribute to bind to query.
- IActionFilters etc. are not supported.
- Swagger custom tag generation containing folders as controllers. (You can check sample project)
- Each endpoint class must have [Endpoint] attribute. You can set custom route override, action name override and HttpMethod.
- You can only set 1 HttpMethod per class.
- The class constructor must be parameterless and public, otherwise it will throw an exception

This project still under development and there might be breaking changes or bugs, be careful when you use it.


## Usage

## Endpoint Attributes

By utilizing these attributes, you can quickly and easily get endpoints created from any file that uses them. By default
the names of the containing folder path and class name will be the names of the endpoints, requiring as little code/effort as possible.

This is a fully functional file that will create a fully functional endpoint for /Sample

```csharp
using AspNetCore.MinimalApi.Ext;

namespace WebApplication1.Endpoints;

[Endpoint]
public class Sample 
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```


#### Set custom HttpMethod

```csharp
using AspNetCore.MinimalApi.Ext;

namespace WebApplication1.Endpoints;

[Endpoint(HttpMethodType.Post)] // Url will be PREFIX/SamplePost
public class SamplePost  
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```

#### Override route

```csharp
using AspNetCore.MinimalApi.Ext;

namespace WebApplication1.Endpoints;

[Endpoint(HttpMethodType.Post, CustomRoute = "Sample/Post")] // Url will be PREFIX/Sample/Post
public class SamplePost 
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```

#### Override action name

```csharp
using AspNetCore.MinimalApi.Ext;

namespace WebApplication1.Endpoints;

[Endpoint(HttpMethodType.Post, ActionName = "Sample_Post")] // Url will be PREFIX/Sample_Post
public class SamplePost 
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```

Every class that has Endpoint attribute will automatically be processed for creating endpoints.
Only one "Handle" method will be considered as an endpoint.
You can change the name of the method by overriding EndpointOptions.

Library does not support multiple endpoints per class.
You can create multiple classes in a single file but not 2 or more endpoints in a single class.

### EndpointAttribute

By using this on attribute;
- You can set HttpMethod which is GET by default.
- You can set custom route override. Which will override the automatic route generation.
- You can set custom action name override. Which will only override the class name in automatic generation. (If you use CustomRoute setting this makes no sense)

If you do not pass any parameters, HttpMethod will be GET and route will be generated automatically.

### EndpointFilterAttribute

By using this on attribute, you can use custom filters that assigned from IEndpointFilter interface.

CustomFilter.cs
```csharp
/// <summary>
///   This is an example of a custom filter that can be used to authorize/validate a request.
/// </summary>
public class CustomFilter : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    var idFromQuery = context.HttpContext.Request.Query["id"].FirstOrDefault();
    if (idFromQuery != "1234") context.HttpContext.Response.StatusCode = 401;
    return await next(context);
  }
}
```
EndpointFile.cs
```csharp
using AspNetCore.MinimalApi.Ext;

namespace WebApplication1.Endpoints;

[Endpoint(HttpMethodType.Post, ActionName = "Sample_Endpoint_With_Filter")] // Url will be PREFIX/Sample_Post
[EndpointFilter(typeof(CustomFilter))]
public class SampleFilterEndpoint
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```

### Why not using default attributes ?

Currently here are the supported attributes;
- [AllowAnonymous]
- [Authorize]

Custom attributes provided by the library currently only way to set Method and route and can only be used on the classes.

### EndpointOptions

You can use EndpointOptions to configure the middleware.

This will add EndpointOptions as singleton to the services.
```csharp
builder.Services.AddMinimalApiEndpointOptions(x => { 
  x.GlobalPrefix = "api/v1"; //Url: /api/Product/Get etc.

  //Global filters
  x.EndpointFilters = new List<Type> { 
      typeof(CustomFilter) 
  };

  //Global authorization filter
  x.AuthorizeData = new AuthorizeData(){
    Policy = "PolicyName"
  };
  
  //Removes "Endpoints" or "Endpoint" string from route
  //Converts auto generated route from /Image/GetImageEndpoint to /Image/GetImage
  x.RemoveEndpointStringInRoute = true; 
  
  //The method name that will be taken and used as endpoint method.
  x.DefaultEndpointMethodName = "Handle";
});
```

### Configuring Swagger Tags
Since the automatic swagger generation will take class names as tags, it won't make sense to use default one.


```csharp
builder.Services.AddSwaggerGen(c => {
        //This will create tags from folder names
    c.ConfigureMinimalApiTags();
});
```
## IBuilderServiceSetup and IApplicationSetup

Any objects that implement IBuilderServiceSetup and/or IApplicationSetup will be processed when the WebApplication is
built. This happens once, when the application starts.

The following sample file (Authentication.cs) sets up azure AD authentication with the application.

```csharp
public class AzureADAuthenticationSetup : IApplicationSetup, IBuilderServiceSetup
{
  
  public int InitializationOrder { get; } = 1; // Initialization order only applies to IApplicationSetup setup
  public void InitializeApplication(WebApplication app)
  {
    var azureAd = app.Configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null) {
      app.UseAuthentication();
      app.UseAuthorization();
    }
  }

  public void InitializeServices(WebApplicationBuilder builder)
  {
    var azureAd = builder.Configuration.GetSection("AzureAd").Get<AzureAd>();

    if (azureAd != null)
      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options => { builder.Configuration.Bind("AzureAd", options); },
          options => { builder.Configuration.Bind("AzureAd", options); });
  }
}
```

### Setup (Program.cs)

This is a complete Program.cs file!

```csharp
    var builder = WebApplication.CreateBuilder(args);

    //if using IBuilderServiceSetup
    builder.UseBuilderSetup();

    var app = builder.Build();

    //if using IApplicationSetup
    app.UseApplicationSetup();

    //if using EndpointAttributes (Minimal API)
    app.UseMinimalApiEndpoints();

    app.Run();
```

### Changelogs

#### 3.2.0
- ReadMe updated
- Attribute changes
- Optimizations
- Custom exceptions
- Removed BaseEndpoint abstract class
- New EndpointOptions

#### 3.0.0

- BuilderSetup changed so that it only asks for WebApplicationBuilder
- Added new attributes for customizing endpoints methods EndpointHttpGet etc.
- Added built-in support for better swagger generation. With few changes you can generate swagger with folders as
  controllers.
- Added new methods that adds Endpoint options as singleton

#### 2.0.0

- Project forked and cleaned up.
- Added support for custom attributes.
- Seperated attributes as a better design choice.
- Better namings
- Forced single endpoint per class.




