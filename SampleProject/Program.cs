using AspNetCore.MinimalApi.Ext.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.UseBuilderSetup();

var app = builder.Build();

app.UseApplicationSetup();

app.Run();