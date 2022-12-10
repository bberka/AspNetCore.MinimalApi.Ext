using SampleProject.Filters;
using Selfrated.MinimalAPI.Middleware.Attributes;

namespace SampleProject.Endpoints;

[EndpointAPI]
public class WithEndpointFilter
{
    [EndpointMethod(EndpointFilters = new[] { typeof(SampleEndpointFilter) })]
    public string HasFilter_IdMustBe1234(string id)
    {
        return $"Hello {id}!";
    }

}
