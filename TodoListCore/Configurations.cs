namespace TodoListCore;

public static class Configurations
{
    public const int DefaultStatusCode = 200;
    public const int defaultpagenumber = 1;
    public const int defaultpagesize = 25;

    public static string ConnectionString { get; set; } = string.Empty;
    public static string BeckEndUrl { get; set; } = string.Empty;
    public static string FrontEndUrl { get; set; } = string.Empty;
}