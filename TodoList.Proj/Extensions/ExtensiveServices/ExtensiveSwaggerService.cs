namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveSwaggerService
{
    public static void SwaggerApplicationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

}