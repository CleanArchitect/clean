using System.Reflection;

namespace Clean.Net.Tests;

[Collection("Extensions")]
public class MethodInfoExtensionsTests
{
    [Fact]
    public void GetParameterTypeThatImplements_ShouldReturnMatchingType()
    {
        MethodInfo method = typeof(TestMethodInfoClass).GetMethod(nameof(TestMethodInfoClass.MethodWithInterface));

        Type result = method.GetParameterTypeThatImplements<IInput>();

        Assert.NotNull(result);
        Assert.Equal(typeof(TestInput), result);
    }

    [Fact]
    public void GetParameterTypeThatImplements_ShouldReturnNull_WhenNoMatch()
    {
        MethodInfo method = typeof(TestMethodInfoClass).GetMethod(nameof(TestMethodInfoClass.MethodWithoutInterface));

        Type result = method.GetParameterTypeThatImplements<IInput>();

        Assert.Null(result);
    }

    [Fact]
    public void GetParameterTypeThatImplements_ShouldReturnCorrectType_WhenMultipleParamsExist()
    {
        MethodInfo method = typeof(TestMethodInfoClass).GetMethod(nameof(TestMethodInfoClass.MethodWithMultipleParams));

        Type result = method.GetParameterTypeThatImplements<IInput>();

        Assert.NotNull(result);
        Assert.Equal(typeof(TestInput), result);
    }

    [Fact]
    public void GetParameterTypeThatImplements_ShouldThrowException_WhenMultipleMatchesExist()
    {
        MethodInfo method = typeof(TestMethodInfoClass).GetMethod(nameof(TestMethodInfoClass.MethodWithMultipleMatchingParams));

        Assert.Throws<InvalidOperationException>(() => method.GetParameterTypeThatImplements<IInput>());
    }

    private class TestInput : IInput { }

    private class TestMethodInfoClass : IInput
    {
        public void MethodWithInterface(TestInput param) { }
        public void MethodWithoutInterface(string param) { }
        public void MethodWithMultipleParams(TestInput param1, string param2) { }
        public void MethodWithMultipleMatchingParams(TestInput param1, TestInput param2) { }
    }
}