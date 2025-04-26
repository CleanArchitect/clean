using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clean.Net;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder WithInputValidation(this RouteGroupBuilder group) =>
        group.AddEndpointFilterFactory(EndpointFilterFactory.InputValidation);
}
