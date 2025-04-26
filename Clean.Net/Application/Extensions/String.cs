namespace Clean.Net;

public static class StringExtensions
{
    public static string MimeType(this string filename) =>
        MimeTypes.Types.TryGetValue(Path.GetExtension(filename), out var mimeType)
            ? mimeType
            : throw new KeyNotFoundException($"MIME type unknown for filename: '{filename}'");
}