namespace TodoList.Proj;

public static class Configuration
{
    public static string JWTKey { get; set; } = Guid.NewGuid().ToString();
}