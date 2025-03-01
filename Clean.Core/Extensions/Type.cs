namespace Clean.Core;

public static class TypeExtensions
{
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