namespace TodoList.Proj;

public static class Configuration
{
    public static string ApiKey = "re_7xTBg8YU_44r7Ng8-GA4w7xwKGr9MvtST";
    public static string JWTKey { get; set; } = Guid.NewGuid().ToString();

    public static SmTpService _SmTpService;
}

public class SmTpService()
{
    public int Port { get; set; } = 587;
    public string Host { get; set; } = "sendservice.shop";
    public string Username { get; set; } = "re_4akpdYni_MXQs71tYtaY92jGQ5AuzRDte";
    public string Password { get; set; } = "Dark1970$$";
}