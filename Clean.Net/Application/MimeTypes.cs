namespace Clean.Net;

public static class MimeTypes
{
    public static readonly string Csv = Types[".csv"];
    public static readonly string Docx = Types[".docx"];
    public static readonly string Jpg = Types[".jpg"];
    public static readonly string Pptx = Types[".pptx"];
    public static readonly string Png = Types[".png"];
    public static readonly string Pdf = Types[".pdf"];
    public static readonly string Rar = Types[".rar"];
    public static readonly string Xlsx = Types[".xlsx"];
    public static readonly string Xml = Types[".xml"];
    public static readonly string Zip = Types[".zip"];

    public static readonly IReadOnlyDictionary<string, string> Types = new Dictionary<string, string>()
    {
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
        { ".webm", "video/webm" },
        { ".webp", "image/webp" },
        { ".woff", "font/woff" },
        { ".woff2", "font/woff2" },
        { ".xhtml", "application/xhtml+xml" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".xml", "application/xml" },
        { ".zip", "application/zip" }
    }.AsReadOnly();
}