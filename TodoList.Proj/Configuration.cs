namespace TodoList.Proj;

public static class Configuration
{
  
    public static SmTpService SmTpService;
}

public class SmTpService()
{
    public int Port { get; set; }
    public string Host { get; set; } 
    public string Username { get; set; }
    public string Password { get; set; }
}