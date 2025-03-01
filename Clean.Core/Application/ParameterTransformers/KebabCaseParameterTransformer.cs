using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Clean.Core;

public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object routeValue) =>
        routeValue == null ? null : ConvertToKebabCase(routeValue.ToString());

    private static string ConvertToKebabCase(string input) =>
        string.IsNullOrEmpty(input)
            ? input
            : Regex.Replace(
                input,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
}