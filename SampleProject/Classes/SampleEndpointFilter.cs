namespace AspNetCore.MinimalApi.Ext.Sample.Classes;

/// <summary>
///   This is an example of a custom filter that can be used to authorize/validate a request.
/// </summary>
public class SampleEndpointFilter : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next) {
    var idFromQuery = context.HttpContext.Request.Query["id"].FirstOrDefault();
    if (idFromQuery != "1234") context.HttpContext.Response.StatusCode = 401;
    return await next(context);
  }
}

