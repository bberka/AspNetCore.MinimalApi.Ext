using AspNetCore.MinimalApi.Ext.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.UseBuilderSetup();

var app = builder.Build();

app.UseApplicationSetup();
app.UseEndpointsAPIAttributes(x => { x.GlobalPrefix = "api"; });

app.Run();