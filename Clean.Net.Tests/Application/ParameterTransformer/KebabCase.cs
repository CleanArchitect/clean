using Clean.Net;

namespace Application.ParameterTransformer;

public class KebabCaseOutputParameterTransformerTests
{
    [Theory]
    [InlineData("Api/TodoItems/GetAll", "api/todo-items/get-all")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void TransformOutbound_ReturnsKebabCaseUri(string route, string expectedResult)
    {
        // arrange
        var transformer = new KebabCaseOutputParameterTransformer();

        // act
        var result = transformer.TransformOutbound(route);

        // assert
        Assert.Equal(expectedResult, result);
    }
}