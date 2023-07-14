using Selfrated.MinimalAPI.Middleware.Attributes;

namespace Selfrated.Middleware.Models;

internal class ExportedTypeResult
{
  public Type Type { get; set; }
  public EndpointAPIAttribute Attribute { get; set; }
}