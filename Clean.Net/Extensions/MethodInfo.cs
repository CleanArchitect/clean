using System.Reflection;

namespace Clean.Net;

public static class MethodInfoExtensions
{
    public static Type GetParameterThatImplements<T>(this MethodInfo methodInfo) =>
        methodInfo
            .GetParameters()
            .Select(parameter => parameter.ParameterType)
            .SingleOrDefault(parameterType => parameterType.Implements(typeof(T)));
}