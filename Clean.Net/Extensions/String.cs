using System.Globalization;
using System.Text.RegularExpressions;

namespace Clean.Net;

public static class CasingStringExtensions
{
    private const string SplitWordsPattern = @"(?<=[\p{Ll}])(?=[\p{Lu}])|(?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])";
    private const string SplitDelimitersPattern = @"[_/\-^'.,=`;:&#\\]+";

    /// <summary>
    /// Converts the input string to kebab case, replacing spaces and other delimiters with hyphens ('-').
    /// </summary>
    /// <returns>
    /// "Hello world" => "Hello-world" <br/>
    /// "some_text_HERE" => "some-text-HERE" <br/>
    /// "ComplexExampleString" => "Complex-Example-String"
    /// </returns>
    public static string ToKebabCase(this string input, params char[] excludeDelimiters) =>
        input?
            .SplitWords()
            .ReplaceDelimiters("-", excludeDelimiters);

    /// <summary>
    /// Converts the input string to snake case, replacing spaces and other delimiters with underscores ('_').
    /// </summary>
    /// <returns>
    /// "Hello world" => "Hello_world" <br/>
    /// "some-text-HERE" => "some_text_HERE" <br/>
    /// "ComplexExampleString" => "Complex_Example_String"
    /// </returns>
    public static string ToSnakeCase(this string input, params char[] excludeDelimiters) =>
        input?
            .SplitWords()
            .ReplaceDelimiters("_", excludeDelimiters);

    /// <summary>
    /// Converts the input string to PascalCase, capitalizing the first letter of each word and removing delimiters.
    /// </summary>
    /// <returns>
    /// "Hello world" => "HelloWorld" <br/>
    /// "some_text_HERE" => "SomeTextHere" <br/>
    /// "complex-example-string" => "ComplexExampleString"
    /// </returns>
    public static string ToPascalCase(this string input, params char[] excludeDelimiters) =>
        CultureInfo.CurrentCulture.TextInfo
            .ToTitleCase(input?.SplitWords().ReplaceDelimiters(" ", excludeDelimiters).ToLower())
            .Replace(" ", "");

    /// <summary>
    /// Converts the input string to camelCase, ensuring the first letter is lowercase and removing delimiters.
    /// </summary>
    /// <returns>
    /// "Hello world" => "helloWorld" <br/>
    /// "some_text_HERE" => "someTextHere" <br/>
    /// "Complex-example-string" => "complexExampleString"
    /// </returns>
    public static string ToCamelCase(this string input, params char[] excludeDelimiters) =>
        input?
            .ToPascalCase(excludeDelimiters)
            .FirstToLower();

    private static string FirstToLower(this string input) =>
        string.IsNullOrWhiteSpace(input)
            ? input
            : char.ToLower(input[0]) + input[1..];

    private static string ReplaceDelimiters(this string input, string replacementDelimiter, params char[] excludeDelimiters) =>
        SplitDelimitersRegex(excludeDelimiters)
            .Replace(input, " ")
            .Trim()
            .Replace(" ", replacementDelimiter);

    private static string SplitWords(this string input) =>
        Regex
            .Replace(input, SplitWordsPattern, " ", RegexOptions.Compiled);

    private static Regex SplitDelimitersRegex(char[] excludeDelimiters) =>
        new(new([.. SplitDelimitersPattern.Where(delimiter => !excludeDelimiters.Contains(delimiter))]));
}