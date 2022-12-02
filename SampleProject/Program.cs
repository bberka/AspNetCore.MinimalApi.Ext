using Selfrated.MinimalAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

//if using IBuilderServiceSetup
builder.UseBuilderSetup();

var app = builder.Build();

//if using IApplicationSetup
app.UseApplicationSetup();

//if using EndpointAttributes (Minimal API)
app.UseEndpointsAPIAttributes();

app.Run();
