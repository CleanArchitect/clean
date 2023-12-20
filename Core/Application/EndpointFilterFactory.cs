using Microsoft.AspNetCore.Http;

namespace Clean.Core;

internal static class EndpointFilterFactory
{
    public static EndpointFilterDelegate InputValidationFilter(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
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
