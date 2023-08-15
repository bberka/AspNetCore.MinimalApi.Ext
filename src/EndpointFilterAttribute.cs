using Microsoft.AspNetCore.Http;

namespace AspNetCore.MinimalApi.Ext;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class EndpointFilterAttribute : Attribute
{
  public EndpointFilterAttribute(Type type) {
    var isTypeIEndpointFilter = typeof(IEndpointFilter).IsAssignableFrom(type);
    if (!isTypeIEndpointFilter)
      throw new ArgumentException($"Type {type.Name} does not implement {nameof(IEndpointFilter)}");
    Type = type;
  }

  public Type Type { get; }
}