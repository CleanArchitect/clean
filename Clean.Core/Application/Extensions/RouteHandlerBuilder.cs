using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clean.Core;

public static class RouteHandlerBuilderExtensions
{
    public static RouteGroupBuilder WithValidation(this RouteGroupBuilder group) =>
        group.AddEndpointFilterFactory(EndpointFilterFactory.InputValidationFilter);
}
