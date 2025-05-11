using System.Globalization;
using System.Text.RegularExpressions;

namespace Clean.Net;

public static class StringExtensions
{
    private const string wordDelimiters = @"[ _/\-^'.,=`;:&#\\]+";

    /// <summary>
    /// Converts the input string to kebab case, replacing spaces or other delimiters with hyphens ('-').
    /// </summary>
    /// <example>
    /// "Hello world" => "Hello-world"
    /// "some_text_HERE" => "some-text-HERE"
    /// "Complex-example-string" => "Complex-example-string"
    /// </example>
    public static string ToKebabCase(this string input, params char[] excludeDelimiters) =>
        input?
            .ToSentence(excludeDelimiters)
            .Replace(' ', '-');

    /// <summary>
    /// Converts the input string to snake case, replacing spaces or other delimiters with underscores ('_').
    /// </summary>
    /// <example>
    /// "Hello world" => "Hello_world"
    /// "some-text-HERE" => "some_text_HERE"
    /// "Complex-example-string" => "Complex_example_string"
    /// </example>
    public static string ToSnakeCase(this string input, params char[] excludeDelimiters) =>
        input?
            .ToSentence(excludeDelimiters)
            .Replace(' ', '_');

    /// <summary>
    /// Converts the input string to PascalCase, capitalizing the first letter of each word and removing delimiters.
    /// </summary>
    /// <example>
    /// "Hello world" => "HelloWorld"
    /// "some_text_HERE" => "SomeTextHere"
    /// "Complex-example-string" => "ComplexExampleString"
    /// </example>
    public static string ToPascalCase(this string input, params char[] excludeDelimiters) =>
        CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(input?.ToSentence(excludeDelimiters).ToLower())
            .Replace(" ", "");

    /// <summary>
    /// Converts the input string to camelCase, ensuring the first letter is lowercase and removing delimiters.
    /// </summary>
    /// <example>
    /// "Hello world" => "helloWorld"
    /// "some_text_HERE" => "someTextHere"
    /// "Complex-example-string" => "complexExampleString"
    /// </example>
    public static string ToCamelCase(this string input, params char[] excludeDelimiters)
    {
        var pascalCase = input?.ToPascalCase(excludeDelimiters);

        if (string.IsNullOrWhiteSpace(pascalCase))
            return pascalCase;

        return char.ToLower(pascalCase[0]) + pascalCase[1..];
    }

    private static string ToSentence(this string input, params char[] excludeDelimiters) =>
        input?
            .DelimitersToSpaces(excludeDelimiters)
            .SplitWordsOnCapitals();

    private static string DelimitersToSpaces(this string input, params char[] excludeDelimiters) =>
        Regex
            .Replace(input, Delimiters(excludeDelimiters), " ", RegexOptions.Compiled)
            .Trim();

    private static string Delimiters(params char[] excludeDelimiters) =>
        new([.. wordDelimiters.Where(delimiter => !excludeDelimiters.Contains(delimiter))]);

    private static string SplitWordsOnCapitals(this string input) =>
        Regex
            .Replace(input, @"(?<=[\p{Ll}])(?=[\p{Lu}])|(?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])", " ", RegexOptions.Compiled);
}