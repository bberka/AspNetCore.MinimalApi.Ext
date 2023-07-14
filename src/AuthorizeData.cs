using Microsoft.AspNetCore.Authorization;

namespace Selfrated.Middleware;

public class AuthorizeData : IAuthorizeData
{
  public string? Policy { get; set; }
  public string? Roles { get; set; }
  public string? AuthenticationSchemes { get; set; }
}