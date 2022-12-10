using Microsoft.Extensions.Caching.Memory;

namespace SampleProject.Filters;

/// <summary>
/// This is an example of a custom filter that can be used to authorize/validate a request.
/// </summary>
public class SampleEndpointFilter : IEndpointFilter
{
    private readonly MemoryCache _cache;

    public SampleEndpointFilter(MemoryCache cache)
    {
        _cache = cache;
    }

    public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
    {
        var id = context.Arguments.FirstOrDefault().ToString();
        
        if (id == "1234")
        {
            return await next(context);
        }
        else
        {
            return Results.Unauthorized();
        }
    }
}

