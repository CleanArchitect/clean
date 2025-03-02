namespace Clean.Core;

public static class StringExtensions
{
    public static string MimeType(this string filename) =>
        MimeTypes.Types.TryGetValue(Path.GetExtension(filename), out var mimeType) ? mimeType : null;
}