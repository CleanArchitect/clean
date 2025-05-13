using Clean.Net;

namespace Extensions;

public class CasingStringExtensionsTests
{
    [Theory]
    [ClassData(typeof(DelimitersTheoryData))]
    public void ToKebabCase_ReturnsExpectedKebab(string delimiter)
    {
        // arrange
        const string expectedKebab = "Kébâb-Cäsè-KÊBAB";

        var input = "  Kébâb CäsèKÊBAB  ".Replace(" ", delimiter);

        // act
        var kebab = input.ToKebabCase();

        // assert
        Assert.Equal(expectedKebab, kebab);
    }

    [Theory]
    [ClassData(typeof(DelimitersTheoryData))]
    public void ToSnakeCase_ReturnsExpectedSnake(string delimiter)
    {
        // arrange
        const string expectedSnake = "Snâké_Cäsè_SNAKÊ";

        var input = "  Snâké CäsèSNAKÊ  ".Replace(" ", delimiter);

        // act
        var snake = input.ToSnakeCase();

        // assert
        Assert.Equal(expectedSnake, snake);
    }

    [Theory]
    [ClassData(typeof(DelimitersTheoryData))]
    public void ToPascalCase_ReturnsExpectedPascal(string delimiter)
    {
        // arrange
        const string expectedPascal = "PâskálCäsèPâscàl";

        var input = "  Pâskál CäsèPÂSCÀL  ".Replace(" ", delimiter);

        // act
        var pascal = input.ToPascalCase();

        // assert
        Assert.Equal(expectedPascal, pascal);
    }

    [Theory]
    [ClassData(typeof(DelimitersTheoryData))]
    public void ToCamelCase_ReturnsExpectedCamel(string delimiter)
    {
        // arrange
        const string expectedCamel = "câmélCäsèCâmèl";

        var input = "  Câmél CäsèCÂMÈL  ".Replace(" ", delimiter);

        // act
        var camel = input.ToCamelCase();

        // assert
        Assert.Equal(expectedCamel, camel);
    }

    private class DelimitersTheoryData : TheoryData<string>
    {
        public DelimitersTheoryData()
        {
            Add(" ");
            Add("_");
            Add("-");
            Add("/");
            Add("\\");
            Add("^");
            Add("'");
            Add(".");
            Add(",");
            Add("=");
            Add("`");
            Add(";");
            Add(":");
            Add("&");
            Add("#");
        }
    }
}