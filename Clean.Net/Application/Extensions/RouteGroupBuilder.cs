using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Clean.Net;

public static class RouteGroupBuilderExtensions
{
    /// <summary>
    /// Adds automatic validation using FluentValidation for <see cref="IInput"/> 
    /// parameters in endpoints.
    /// </summary>
    public static RouteGroupBuilder WithInputValidation(this RouteGroupBuilder group) =>
        group.AddEndpointFilterFactory(ValidationEndpointFilterFactory.InputValidation);
}
