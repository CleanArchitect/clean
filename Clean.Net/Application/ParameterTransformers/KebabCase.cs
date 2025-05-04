using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Clean.Net;

/// <summary>
/// An implementation of <see cref="IOutboundParameterTransformer"/> 
/// that converts route values from PascalCase to lowercase kebab.
/// For example API route: /Api/TodoItems/ becomes /api/todo-items/
/// </summary>
public class KebabCaseOutputParameterTransformer : IOutboundParameterTransformer
{
    private static readonly string pascalCaseSplitRegex = "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])";

    public string TransformOutbound(object routeValue) =>
        routeValue == null ? null : ConvertToKebabCase(routeValue.ToString());

    private static string ConvertToKebabCase(string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : Regex.Replace(
                input,
                pascalCaseSplitRegex,
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
}