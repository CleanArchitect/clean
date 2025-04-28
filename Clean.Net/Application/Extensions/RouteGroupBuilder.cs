using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clean.Net;

public static class RouteGroupBuilderExtensions
{
    /// <summary>
    /// Adds automatic validation for <see cref="IInput"/> endpoint parameters.
    /// </summary>
    public static RouteGroupBuilder WithInputValidation(this RouteGroupBuilder group) =>
        group.AddEndpointFilterFactory(EndpointFilterFactory.InputValidation);
}
