using System.Text.RegularExpressions;

namespace Clean.Net;

public static class StringExtensions
{
    public static string ToKebabCase(this string value) =>
        string.IsNullOrEmpty(value)
            ? value
            : Regex.Replace(
                value.Replace("_", "-").Replace(" ", "-"),
                @"(?<=[a-z])(?=[A-Z])",
                "-", RegexOptions.Compiled);

    public static string ToSnakeCase(this string value) =>
        string.IsNullOrEmpty(value)
            ? value
            : Regex.Replace(
                value.Replace("-", "_").Replace(" ", "_"),
                @"(?<=[a-z])(?=[A-Z])",
                "_", RegexOptions.Compiled);

    public static string ToPascalCase(this string value) =>
        string.IsNullOrEmpty(value)
            ? value
            : string.Concat(
                Regex.Split(value.ToLower(), @"[\s\-_]+") // Normalize to lowercase & split on spaces, hyphens, underscores
                    .Select(word => char.ToUpperInvariant(word[0]) + word.Substring(1)));

    public static string ToCamelCase(this string value) =>
        string.IsNullOrEmpty(value) ? value : char.ToLowerInvariant(ToPascalCase(value)[0]) + ToPascalCase(value)[1..];
}