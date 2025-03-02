namespace Clean.Core;

public static class MimeTypes
{
    public static Dictionary<string, string> Types = new()
    {
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".pdf", "application/pdf" },
        { ".png", "image/png" }
    };
}