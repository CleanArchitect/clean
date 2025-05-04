using Microsoft.AspNetCore.Http;
using System.Net;

namespace Clean.Net;

internal sealed class InputFluentValidationFilter<TInput> : IEndpointFilter where TInput : IInput, new()
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validationResult = await context.ValidateAsync<TInput>();

        return validationResult == null || validationResult?.IsValid == true
            ? await next.Invoke(context)
            : Results.ValidationProblem(validationResult.ToDictionary(), statusCode: (int)HttpStatusCode.BadRequest);
    }
}