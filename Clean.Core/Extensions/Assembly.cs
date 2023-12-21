using System.Reflection;

namespace Clean.Core;

public static class AssemblyExtensions
{
    public static IEnumerable<Type> GetImplementations(this Assembly assembly, Type interfaceType) =>
        assembly
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => !type.IsAbstract)
            .Where(type => !type.IsGenericType)
            .Where(type => type.Implements(interfaceType));
}
