# Minimal API Middleware

A set of tools to simplify creating ASPNetCore applications, specifically when using MinimalAPIs.

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

This is a fork from original project there is no package yet. You can only install it from github.

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





