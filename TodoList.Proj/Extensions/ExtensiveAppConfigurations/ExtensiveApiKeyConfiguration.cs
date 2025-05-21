namespace TodoList.Proj.Extensions.ExtensiveAppConfigurations;

public static class ExtensiveApiKeyConfiguration
{
    public static void ConfigurationsJSONSApiKey(this WebApplication builder)
    {
        builder.Configuration.GetValue<string>("apikey");
    }
}