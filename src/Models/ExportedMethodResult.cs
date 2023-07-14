using System.Reflection;
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace Selfrated.Middleware.Models;

public class ExportedMethodResult
{
  public MethodInfo Method { get; set; }
  public EndpointMethodAttribute? Attribute { get; set; }
}