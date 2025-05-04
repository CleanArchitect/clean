using Microsoft.Extensions.Configuration;

namespace Clean.Net;

internal static class BindOptionsExtentions
{
    public static void WithBindNonPublicProperties(this BinderOptions defaultOptions, Action<BinderOptions> options)
    {
        defaultOptions.BindNonPublicProperties = true;
        options?.Invoke(defaultOptions);
    }
}