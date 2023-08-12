using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointRouteAttribute : Attribute
{
  internal EndpointRouteAttribute(RouteAttribute routeAttribute)
  {
    Template = routeAttribute.Template;
    Name = routeAttribute.Name;
  }
  public EndpointRouteAttribute(string template)
  {
    Template = template;
  }
  public string Template { get; }
  public string? Name { get; set; }
}