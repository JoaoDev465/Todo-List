namespace TodoList.Proj.Extensions.ExtensiveAppConfigurations;

public static class ExtensiveAuthenticationApp
{
    public static void AuthenticantionAndAuthorization(this WebApplication builder)
    {
        builder.UseAuthentication();
        builder.UseAuthorization(); 
    }

}