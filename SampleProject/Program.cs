using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Selfrated.Middleware;
using Selfrated.MinimalAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.UseBuilderSetup();

var app = builder.Build();

app.UseApplicationSetup();
app.UseEndpointsAPIAttributes(x => {
  x.GlobalPrefix = "api";
  
});

app.Run();
