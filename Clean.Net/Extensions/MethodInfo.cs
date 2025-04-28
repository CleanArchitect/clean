using System.Reflection;

namespace Clean.Net;

public static class MethodInfoExtensions
{
    /// <summary>
    /// Retrieves the type of a parameter in the specified method that implements the given generic type or interface <typeparamref name="TInterface"/>.
    /// </summary>
    /// <typeparam name="TInterface">The type of interface that the parameter type must implement.</typeparam>
    /// <param name="methodInfo">The <see cref="MethodInfo"/> representing the method to inspect.</param>
    /// <returns>
    /// The Type of the parameter in the method that implements <typeparamref name="TInterface"/>.
    /// Returns null if no such parameter is found.
    /// </returns>
    public static Type GetParameterTypeThatImplements<TInterface>(this MethodInfo methodInfo) =>
        methodInfo
            .GetParameters()
            .Select(parameter => parameter.ParameterType)
            .SingleOrDefault(parameterType => parameterType.Implements(typeof(TInterface)));
}