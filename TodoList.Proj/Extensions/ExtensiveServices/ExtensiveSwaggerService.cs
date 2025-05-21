namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveSwaggerService
{
    public static void SwaggerAplicationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

}