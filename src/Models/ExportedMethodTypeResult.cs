using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.MinimalApi.Ext.Models;

public class ExportedMethodTypeResult
{
  public ExportedMethodTypeResult(MethodInfo methodInfo)
  {
    MethodInfo = methodInfo;
    Authorize = methodInfo.GetCustomAttribute<AuthorizeAttribute>();
    var allowAnonymous = methodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
    if (allowAnonymous is not null) Authorize = null;
    Filters = methodInfo.GetCustomAttributes<EndpointFilterAttribute>().Select(x => x.Type).ToArray();
    Endpoint = methodInfo.GetCustomAttribute<EndpointAttribute>();
  }
  public EndpointAttribute? Endpoint { get;  }
  public MethodInfo MethodInfo { get;  }
  public Type[] Filters { get;  }
  public IAuthorizeData? Authorize { get; }

}