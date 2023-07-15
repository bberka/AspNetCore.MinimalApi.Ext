using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class EndpointAuthorizeAttribute : AuthorizeAttribute
{
}