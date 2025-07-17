namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveSwaggerService
{
    public static void SwaggerApplicationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

}