namespace Clean.Net;

/// <summary>
/// Contains some basic MIME types for most common file types.
/// Extend on this partial class with your own used MIME types if needed.
/// </summary>
public static partial class MimeType
{
    public static readonly string Csv = Types[".csv"];
    public static readonly string Docx = Types[".docx"];
    public static readonly string Jpg = Types[".jpg"];
    public static readonly string Pptx = Types[".pptx"];
    public static readonly string Png = Types[".png"];
    public static readonly string Pdf = Types[".pdf"];
    public static readonly string Rar = Types[".rar"];
    public static readonly string Txt = Types[".txt"];
    public static readonly string Xlsx = Types[".xlsx"];
    public static readonly string Xml = Types[".xml"];
    public static readonly string Zip = Types[".zip"];

    /// <summary>
    /// Dictionary with most common file extensions and their MIME types (based on MDN docs).
    /// Key is file extension with . included, for example ".png".
    /// Add an extension and MIME type if needed at startup of your application.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Guides/MIME_types/Common_types"/>
    /// </summary>
    public static readonly Dictionary<string, string> Types = new()
    {
        { ".7z", "application/x-7z-compressed" },
        { ".aac", "audio/aac" },
        { ".abw", "application/x-abiword" },
        { ".apk", "application/vnd.android.package-archive" },
        { ".arc", "application/x-freearc" },
        { ".avi", "video/x-msvideo" },
        { ".azw", "application/vnd.amazon.ebook" },
        { ".bat", "application/x-msdownload" },
        { ".bin", "application/octet-stream" },
        { ".bmp", "image/bmp" },
        { ".bz", "application/x-bzip" },
        { ".bz2", "application/x-bzip2" },
        { ".csh", "application/x-csh" },
        { ".css", "text/css" },
        { ".csv", "text/csv" },
        { ".deb", "application/vnd.debian.binary-package" },
        { ".dll", "application/x-msdownload" },
        { ".doc", "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".dmg", "application/x-apple-diskimage" },
        { ".eot", "application/vnd.ms-fontobject" },
        { ".epub", "application/epub+zip" },
        { ".exe", "application/x-msdownload" },
        { ".flac", "audio/flac" },
        { ".gif", "image/gif" },
        { ".gz", "application/gzip" },
        { ".heic", "image/heic" },
        { ".heif", "image/heif" },
        { ".html", "text/html" },
        { ".ico", "image/vnd.microsoft.icon" },
        { ".ics", "text/calendar" },
        { ".iso", "application/x-iso9660-image" },
        { ".jar", "application/java-archive" },
        { ".java", "text/x-java-source" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".js", "text/javascript" },
        { ".json", "application/json" },
        { ".jsonld", "application/ld+json" },
        { ".md", "text/markdown" },
        { ".mid", "audio/midi" },
        { ".midi", "audio/x-midi" },
        { ".mjs", "text/javascript" },
        { ".mov", "video/quicktime" },
        { ".mp3", "audio/mpeg" },
        { ".mp4", "video/mp4" },
        { ".mpeg", "video/mpeg" },
        { ".mpkg", "application/vnd.apple.installer+xml" },
        { ".odp", "application/vnd.oasis.opendocument.presentation" },
        { ".ods", "application/vnd.oasis.opendocument.spreadsheet" },
        { ".odt", "application/vnd.oasis.opendocument.text" },
        { ".oga", "audio/ogg" },
        { ".ogv", "video/ogg" },
        { ".ogx", "application/ogg" },
        { ".opus", "audio/opus" },
        { ".otf", "font/otf" },
        { ".pdf", "application/pdf" },
        { ".php", "application/x-httpd-php" },
        { ".png", "image/png" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".ps", "application/postscript" },
        { ".rar", "application/vnd.rar" },
        { ".rtf", "application/rtf" },
        { ".sh", "application/x-sh" },
        { ".sql", "application/sql" },
        { ".svg", "image/svg+xml" },
        { ".tar", "application/x-tar" },
        { ".ttf", "font/ttf" },
        { ".txt", "text/plain" },
        { ".webm", "video/webm" },
        { ".webp", "image/webp" },
        { ".woff", "font/woff" },
        { ".woff2", "font/woff2" },
        { ".xhtml", "application/xhtml+xml" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".xml", "application/xml" },
        { ".zip", "application/zip" }
    };

    /// <summary>
    /// Get the MIME type for a given filename with file extension including dot.
    /// Use <see cref="Types"/> instead if you know the file extension.
    /// </summary>
    /// <param name="filename">String containing a filename (with extension)</param>
    /// <returns>The MIME type for found extension in string or null when no extension in <paramref name="filename"/>></returns>
    /// <exception cref="KeyNotFoundException">When extension in filename but unknown in <see cref="Types"/></exception>
    public static string ToMimeType(this string filename)
    {
        var extension = Path.GetExtension(filename);

        if (string.IsNullOrWhiteSpace(extension))
            return null;

        return Types.TryGetValue(extension, out var mimeType)
            ? mimeType
            : throw new KeyNotFoundException($"MIME type unknown for extension: '{extension}'. Add extension/MIME type to MimeType.Types dictionary at startup.");
    }
}