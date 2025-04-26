using System.Reflection;

namespace Clean.Net;

public static class AssemblyExtensions
{
    /// <summary>
    /// Retrieves all concrete, non-generic types in the specified assembly that implement the given interface type.
    /// This method filters out abstract and generic types, returning only valid implementations of <paramref name="interfaceType"/>.
    /// </summary>
    /// <param name="assembly">The assembly to scan for implementations.</param>
    /// <param name="interfaceType">The interface type whose implementations should be retrieved.</param>
    /// <returns>
    /// A collection of <see cref="Type"/> representing classes that implement <paramref name="interfaceType"/>.
    /// </returns>
    /// <remarks>
    /// - Only concrete classes (non-abstract) are included.
    /// - Open generic types are excluded.
    /// - If no implementations are found, an empty collection is returned.
    /// </remarks>
    public static IEnumerable<Type> GetImplementations(this Assembly assembly, Type interfaceType) =>
        assembly
            .GetTypes()
            .Where(type => type.IsClass)
            .Where(type => !type.IsAbstract)
            .Where(type => !type.IsGenericType)
            .Where(type => type.Implements(interfaceType));
}
