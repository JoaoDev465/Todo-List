namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensivesCorsServices 
{
    public static void CorsServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(x => x.AddPolicy(
            "allow", policyBuilder =>
                policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
    }
}