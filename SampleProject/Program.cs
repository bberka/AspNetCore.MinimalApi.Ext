using Microsoft.AspNetCore.Builder;
using Selfrated.MinimalAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.UseBuilderSetup();

var app = builder.Build();

app.UseApplicationSetup();
app.UseEndpointsAPIAttributes();

app.Run();
