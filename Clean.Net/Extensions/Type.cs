namespace Clean.Net;

public static class TypeExtensions
{
    /// <summary>
    /// Determines whether the specified type implements the given interface type. 
    /// Supports open generic types.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> to check for implementation.</param>
    /// <param name="interfaceType">The <see cref="Type"/> representing the interface type to verify implementation against.</param>
    /// <returns>
    /// True if the specified type implements the given interface or generic type; otherwise, False.
    /// </returns>
    public static bool Implements(this Type type, Type interfaceType) =>
        interfaceType.IsGenericType
            ? type.ImplementsGenericInterface(interfaceType)
            : type.ImplementsInterface(interfaceType);

    private static bool ImplementsInterface(this Type type, Type interfaceType) =>
        type
            .GetInterfaces()
            .Contains(interfaceType);

    private static bool ImplementsGenericInterface(this Type type, Type interfaceType) =>
        type
            .GetInterfaces()
            .Where(typeInterfaceType => typeInterfaceType.IsGenericType)
            .Select(typeInterfaceType => typeInterfaceType.GetGenericTypeDefinition())
            .Contains(interfaceType);
}