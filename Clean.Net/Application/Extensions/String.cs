namespace Clean.Net;

public static class StringExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename">a string containing a filename (with extension)</param>
    /// <returns>a MIME type string</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public static string MimeType(this string filename) =>
        Net.MimeType.Types.TryGetValue(Path.GetExtension(filename), out var mimeType)
            ? mimeType
            : throw new KeyNotFoundException($"MIME type unknown for filename: '{filename}'");
}