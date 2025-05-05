using Microsoft.AspNetCore.Http;

namespace Clean.Net;

internal static class ValidationEndpointFilterFactory
{
    public static EndpointFilterDelegate InputValidation(EndpointFilterFactoryContext context, EndpointFilterDelegate next)
    {
        var inputType = context.MethodInfo.GetParameterTypeThatImplements<IInput>();

        return async invocationContext => inputType is not null
            ? await CreateFluentValidationEndpointFilter(inputType).InvokeAsync(invocationContext, next)
            : await next(invocationContext);
    }

    private static IEndpointFilter CreateFluentValidationEndpointFilter(Type inputType) =>
        Activator
            .CreateInstance(typeof(InputFluentValidationFilter<>)
                .MakeGenericType(inputType)) as IEndpointFilter;
}
