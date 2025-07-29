namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensivesCorsServices 
{
    public static void CorsServices(this WebApplicationBuilder builder)
    {
        string? ConnectionCors = builder.Configuration.GetValue<string>("BeckEndCorsName");
        builder.Services.AddCors(x => x.AddPolicy(
            ConnectionCors, policyBuilder =>
                policyBuilder.AllowAnyHeader().AllowAnyMethod()));
    }
}