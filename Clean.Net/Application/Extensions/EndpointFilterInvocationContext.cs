using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

internal static class EndpointFilterInvocationContextExtensions
{
    public static async Task<ValidationResult> ValidateAsync<T>(this EndpointFilterInvocationContext context) where T : new()
    {
        var input = context.GetArgumentOfType<T>();
        var validator = context.GetService<IValidator<T>>();

        return validator != null
            ? await validator.ValidateAsync(input)
            : null;
    }

    public static TService GetService<TService>(this EndpointFilterInvocationContext context) =>
        context
            .HttpContext
            .RequestServices
            .GetService<TService>();

    public static T GetArgumentOfType<T>(this EndpointFilterInvocationContext context) where T : new() =>
        context
            .Arguments
            .OfType<T>()
            .SingleOrDefault() ?? new T();
}