namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensivesCorsServices 
{
    public static void CorsServices(this WebApplicationBuilder builder)
    {
        string? connectionCors = builder.Configuration.GetValue<string>("BeckEndCorsName");
        builder.Services.AddCors(x => x.AddPolicy(
            "allow", policyBuilder =>
                policyBuilder.AllowAnyHeader().AllowAnyMethod()));
    }
}