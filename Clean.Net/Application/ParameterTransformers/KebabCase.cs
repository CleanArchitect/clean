using Microsoft.AspNetCore.Routing;

namespace Clean.Net;

/// <summary>
/// An implementation of <see cref="IOutboundParameterTransformer"/> 
/// that converts route values from PascalCase to lowercase kebab.
/// </summary>
/// <remarks>
/// MyExamplesController => "my-examples" 
/// </remarks>
public class KebabCaseOutboundParameterTransformer : IOutboundParameterTransformer
{
    public string TransformOutbound(object routeValue) =>
        routeValue?
            .ToString()?
            .ToKebabCase('/')
            .ToLower();
}