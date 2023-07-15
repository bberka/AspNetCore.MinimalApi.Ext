using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext;

public class AuthorizeData : IAuthorizeData
{
  public string? Policy { get; set; }
  public string? Roles { get; set; }
  public string? AuthenticationSchemes { get; set; }
}