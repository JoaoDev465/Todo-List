namespace TodoList.Proj;

public static class Configuration
{
    public static string JWTKey { get; set; } = Guid.NewGuid().ToString();

    public static SmTpService _SmTpService;
}

public class SmTpService()
{
    public int Port { get; set; }
    public string Host { get; set; } 
    public string Username { get; set; }
    public string Password { get; set; }
}