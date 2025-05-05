namespace Clean.Net.Tests;

[Collection("Extensions")]
public class StringExtensionsTests
{
    [Theory]
    [InlineData("KEBAB CASE", "KEBAB-CASE")]
    [InlineData("KebabCase", "Kebab-Case")]
    [InlineData("Kebab-Case", "Kebab-Case")]
    [InlineData("kebab case", "kebab-case")]
    [InlineData("Kebab CaseKebab", "Kebab-Case-Kebab")]
    [InlineData("Kebab Case Kebab", "Kebab-Case-Kebab")]
    [InlineData("Kebab Case-Kebab", "Kebab-Case-Kebab")]
    [InlineData("Kebab_Case-Kebab", "Kebab-Case-Kebab")]
    public void ToKebabCase_Returns_Expected_Kebab(string input, string expectedKebab)
    {
        // arrange & act
        var kebab = input.ToKebabCase();

        // assert
        Assert.Equal(expectedKebab, kebab);
    }

    [Theory]
    [InlineData("SNAKE CASE", "SNAKE_CASE")]
    [InlineData("SnakeCase", "Snake_Case")]
    [InlineData("Snake-Case", "Snake_Case")]
    [InlineData("snake case", "snake_case")]
    [InlineData("snake_case", "snake_case")]
    [InlineData("Snake CaseSnake", "Snake_Case_Snake")]
    [InlineData("Snake Case Snake", "Snake_Case_Snake")]
    [InlineData("Snake Case-Snake", "Snake_Case_Snake")]
    [InlineData("Snake_Case-Snake", "Snake_Case_Snake")]
    public void ToSnakeCase_Returns_Expected_Snake(string input, string expectedSnake)
    {
        // arrange & act
        var snake = input.ToSnakeCase();

        // assert
        Assert.Equal(expectedSnake, snake);
    }

    [Theory]
    [InlineData("PASCAL CASE", "PascalCase")]
    [InlineData("PascalCase", "PascalCase")]
    [InlineData("Pascal-Case", "PascalCase")]
    [InlineData("pascal case", "PascalCase")]
    [InlineData("Pascal CasePascal", "PascalCasePascal")]
    [InlineData("Pascal Case Pascal", "PascalCasePascal")]
    [InlineData("Pascal Case-Pascal", "PascalCasePascal")]
    [InlineData("Pascal_Case-Pascal", "PascalCasePascal")]
    public void ToPascalCase_Returns_Expected_Pascal(string input, string expectedPascal)
    {
        // arrange & act
        var pascal = input.ToPascalCase();

        // assert
        Assert.Equal(expectedPascal, pascal);
    }
}