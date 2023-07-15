using AspNetCore.MinimalApi.Ext.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.UseBuilderSetup();

var app = builder.Build();

app.UseApplicationSetup();

app.Run();