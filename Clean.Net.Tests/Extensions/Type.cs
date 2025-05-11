using Clean.Net;

namespace Extensions;

public class TypeExtensionTests
{
    [Theory]
    [InlineData(typeof(TestTypeClass), typeof(ITestTypeInterface))]
    [InlineData(typeof(TestTypeClass), typeof(IGenericTestTypeInterface<>))]
    [InlineData(typeof(GenericTestTypeClass<>), typeof(IGenericTestTypeInterface<>))]
    [InlineData(typeof(GenericTestTypeClass<int>), typeof(IGenericTestTypeInterface<>))]
    public void Implements_ShouldReturnTrue_WhenTypeImplementsInterface(Type type, Type interfaceType)
    {
        // arrange, act & assert
        Assert.True(type.Implements(interfaceType));
    }

    [Theory]
    [InlineData(typeof(TestTypeClass), typeof(IDisposable))]
    [InlineData(typeof(TestTypeClass), typeof(IList<>))]
    [InlineData(typeof(GenericTestTypeClass<>), typeof(ITestTypeInterface))]
    public void Implements_ShouldReturnFalse_WhenTypeDoesNotImplementInterface(Type type, Type interfaceType)
    {
        // arrange, act & assert
        Assert.False(type.Implements(interfaceType));
    }

    private interface ITestTypeInterface { }
    private interface IGenericTestTypeInterface<T> { }
    private class TestTypeClass : ITestTypeInterface, IGenericTestTypeInterface<int> { }
    private class GenericTestTypeClass<T> : IGenericTestTypeInterface<T> { }
}