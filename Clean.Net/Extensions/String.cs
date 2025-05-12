using System.Globalization;
using System.Text.RegularExpressions;

namespace Clean.Net;

public static class StringExtensions
{
    /// <summary>
    /// Converts the input string to kebab case, replacing spaces or other delimiters with hyphens ('-').
    /// </summary>
    /// <returns>
    /// "Hello world" => "Hello-world" <br/>
    /// "some_text_HERE" => "some-text-HERE" <br/>
    /// "ComplexExampleString" => "Complex-Example-String"
    /// </returns>
    public static string ToKebabCase(this string input, params char[] excludeDelimiters) =>
        input?
            .ToSentence(excludeDelimiters)
            .Replace(' ', '-');

    /// <summary>
    /// Converts the input string to snake case, replacing spaces or other delimiters with underscores ('_').
    /// </summary>
    /// <returns>
    /// "Hello world" => "Hello_world" <br/>
    /// "some-text-HERE" => "some_text_HERE" <br/>
    /// "ComplexExampleString" => "Complex_Example_String"
    /// </returns>
    public static string ToSnakeCase(this string input, params char[] excludeDelimiters) =>
        input?
            .ToSentence(excludeDelimiters)
            .Replace(' ', '_');

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
            .ToTitleCase(input?.ToSentence(excludeDelimiters).ToLower())
            .Replace(" ", "");

    /// <summary>
    /// Converts the input string to camelCase, ensuring the first letter is lowercase and removing delimiters.
    /// </summary>
    /// <returns>
    /// "Hello world" => "helloWorld" <br/>
    /// "some_text_HERE" => "someTextHere" <br/>
    /// "Complex-example-string" => "complexExampleString"
    /// </returns>
    public static string ToCamelCase(this string input, params char[] excludeDelimiters)
    {
        var pascalCase = input?
            .ToPascalCase(excludeDelimiters);

        if (string.IsNullOrWhiteSpace(pascalCase))
            return pascalCase;

        return char.ToLower(pascalCase[0]) + pascalCase[1..];
    }

    /// <summary>
    /// Converts the input string to a sentence, all delimiters are replaced by spaces 
    /// and words are split up by PascalCase. Undo all casing like kebab, snake, camel or pascal.
    /// </summary>
    /// <returns>
    /// "HelloWorld" => "Hello World" <br/>
    /// "some_text_HERE" => "some text HERE" <br/>
    /// "Complex-example-string" => "Complex Example String"
    /// </returns>
    public static string ToSentence(this string input, params char[] excludeDelimiters) =>
        input?
            .ReplaceDelimiters(excludeDelimiters)
            .SplitPascalCase();

    private static string ReplaceDelimiters(this string input, params char[] excludeDelimiters) =>
        Regex
            .Replace(input, $"[{new([.. @"_/\-^'.,=`;:&#\\".Where(delimiter => !excludeDelimiters.Contains(delimiter))])}]+", " ", RegexOptions.Compiled)
            .Trim();

    private static string SplitPascalCase(this string input) =>
        Regex
            .Replace(input, @"(?<=[\p{Ll}])(?=[\p{Lu}])|(?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])", " ", RegexOptions.Compiled);
}