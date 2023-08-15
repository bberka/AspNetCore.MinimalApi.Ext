# Minimal API Middleware

A set of tools to simplify creating AspNetCore applications, specifically when using MinimalAPIs.

The middleware helps clean up your code by making it easy to break the application startup into seperate classes,
ideally named by what their purpose is.

## Why forked ?

- Code is cleaned up and simplified.
- Added support for custom attributes.
- Seperated attributes as a better design choice.
- Better namings
- Forced single endpoint per class.

## Reqiurements

ASPNetCore applications, with at least .NET 7.0

## Installation Instructions

Install the package from NuGet:

```bash
Install-Package AspNetCore.MinimalApi.Ext
```

Install the package from CLI:
```bash
dotnet add package AspNetCore.MinimalApi.Ext
```

## Usage


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

## Endpoint Attributes

By utilizing these attributes, you can quickly and easily get endpoints created from any file that uses them. By default
the names of the classes/methods will be the names of the endpoints, requiring as little code/effort as possible.

This is a fully functional file that will create a fully functional endpoint for /Sample/TestEndpoint (
i.e. https://localhost:7000/Sample/TestEndpoint)!

```csharp
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace WebApplication1.Endpoints;


public class Sample : BaseEndpoint
{
    public string Handle(HttpContext context)
    {
        return "Hello World!";
    }   
}
```

Every class that has BaseEndpoint as a parent will automatically be processed for creating endpoints.
Only Handle method will be considered as an endpoint.

Library forces you to have single endpoint per class.
You can create multiple classes in a single file but not 2 or more endpoints in a single class.

### EndpointAuthorizeAttribute

By using this on class, it will require authorization to access the endpoint.

### EndpointFilterAttribute

By using this on class, you can use custom filters that assigned from IEndpointFilter

### EndpointRouteAttribute

By using this on class, you can override the route of the endpoint.

### EndpointHttpMethodAttribute

By using this on class, you can override the http method of the endpoint.
You can use multiple and if you use none it will be GET by default.

### Why not using default attributes ?

Currently default attributes provided by AspNetCore is not supported but support can be added easily.

Custom attributes provided by the library currently only way to go and can only be used on the class not method.

### EndpointMiddlewareOptions

You can use EndpointMiddlewareOptions to configure the middleware.

```csharp
app.UseMinimalApiEndpoints(x => { 
  x.GlobalPrefix = "api"; //Url: /api/Product/Get etc.

  //Global filters
  x.EndpointFilters = new List<Type> { typeof(CustomFilter) };

  //Global authorization
  x.AuthorizeData = new AuthorizeData(){
    Policy = "PolicyName"
  };
});
```

### What is not supported ?

- Currently only class level custom attributes are supported.
- Model binding to query is not supported however you can use FromQueryAttribute to bind to query.
- IActionFilters etc. are not supported.
- Swagger generation with folders as controllers

## IBuilderServiceSetup and IApplicationSetup

Any objects that implement IBuilderServiceSetup and/or IApplicationSetup will be processed when the WebApplication is
built. This happens once, when the applcation starts.

The following sample file (Authentication.cs) sets up azure AD authentication with the application.

```csharp
public class Authentication : IApplicationSetup, IBuilderServiceSetup
{
    public void InitializeApplication(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public void InitializeServices(IServiceCollection services, ConfigurationManager configuration, ConfigureHostBuilder host)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(options =>
        {
            configuration.Bind("AzureAd", options);
        },
        options => { configuration.Bind("AzureAd", options); });
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




