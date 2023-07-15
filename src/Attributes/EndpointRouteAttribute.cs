namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointRouteAttribute : Attribute
{
  public EndpointRouteAttribute(string template) {
    Template = template;
  }

  public string Template { get; }
  public string? Name { get; set; }
}