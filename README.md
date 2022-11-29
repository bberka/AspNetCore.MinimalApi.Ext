# Minimal API Middleware

A set of tools to simplify creating ASPNetCore applications, specifically when using MinimalAPIs.

The middleware helps clean up your code by making it easy to break the application startup into seperate classes, ideally named by what their purpose is. 


### Reqiurements
and ASPNetCore applications, with at least .NET 7.0

### Installation Instructions

```shell
dotnet add package Selfrated.MinimalAPI.Middleware
```

## Endpoint Attributes
By utilizing these attributes, you can quickly and easily get endpoints created from any file that uses them. By default the names of the classes/methods will be the names of the endpoints, requiring as little code/effort as possible.

This is a fully functional file that will create a fully functional endpoint for /Sample/TestEndpoint (i.e. https://localhost:7000/Sample/TestEndpoint)!

```csharp
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace WebApplication1.Endpoints;

[EndpointAPI]
public class Sample
{
    [EndpointMethod]
    public string TestEndpoint()
    {
        return "Hello World!";
    }
}
```

### EndpointAPIAttribute
Any class that has wants to expose an endpoint will need to have this attribute defined. 

#### Optional Parameters
* Name - set this if you don't want to use the class name. Setting as null will exlude the path prefix (i.e. /Sample/TestEndpoint will become /TestEndpoint)
* AuthenticationRequired - Default No. This will apply to all child methods, unless explicitly stated by the method
* Roles - Array of role names required for authorization.

#### Sample with override
```csharp
[EndpointAPI(Name = "NewPrefix", AuthenticationRequired = AuthenticationRequired.Yes)]
```

### EndpointMethodAttribute
By using this on any method in a class that uses EndpointAPIAttribute, it will create an endpoint just like that!

#### Optional Parameters
* Name - set this if you don't want to use the class name.
* AuthenticationRequired - Default Inherit.
* Roles - Array of role names required for authorization.
* MethodType - Default Post. Can combine using bitwise OR operator (i.e MethodTypeEnum.Post | MethodTypeEnum.Get)
* PrefixOverride - Override the url prefix from the parent.
* EndpointFilter - Type that implements IEndpointFilter

#### Sample with parameters
```csharp
[EndpointMethod(Name = "NewName", MethodType = MethodTypeEnum.POST | MethodTypeEnum.PUT)]
```

## IBuilderServiceSetup and IApplicationSetup

Any objects that implement IBuilderServiceSetup and/or IApplicationSetup will be processed when the WebApplication is built. This happens once, when the applcation starts.

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
    app.UseEndpoints();

    app.Run();
```





