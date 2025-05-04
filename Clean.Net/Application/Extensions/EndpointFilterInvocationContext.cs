using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

internal static class EndpointFilterInvocationContextExtensions
{
    public static async Task<ValidationResult> ValidateAsync<TInput>(this EndpointFilterInvocationContext context) where TInput : IInput, new()
    {
        var input = context.GetArgumentOfType<TInput>();
        var validator = context.GetService<IValidator<TInput>>();

        return validator != null
            ? await validator.ValidateAsync(input)
            : new ValidationResult();
    }

    private static TService GetService<TService>(this EndpointFilterInvocationContext context) =>
        context
            .HttpContext
            .RequestServices
            .GetService<TService>();

    private static T GetArgumentOfType<T>(this EndpointFilterInvocationContext context) where T : new() =>
        context
            .Arguments
            .OfType<T>()
            .SingleOrDefault() ?? new T();
}