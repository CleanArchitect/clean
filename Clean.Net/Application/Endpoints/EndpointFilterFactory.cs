using Microsoft.AspNetCore.Http;

namespace Clean.Net;

internal static class EndpointFilterFactory
{
    public static EndpointFilterDelegate InputValidation(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        var inputType = context.MethodInfo.GetParameterThatImplements<IInput>();

        return async invocationContext => inputType != null
            ? await CreateValidationFilter(inputType).InvokeAsync(invocationContext, next)
            : await next(invocationContext);
    }

    private static IEndpointFilter CreateValidationFilter(Type inputType) =>
        Activator
            .CreateInstance(typeof(InputValidationFilter<>)
                .MakeGenericType(inputType)) as IEndpointFilter;
}
