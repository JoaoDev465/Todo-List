using System.Linq.Expressions;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensivesCorsServices 
{
    public static void CorsServices(this WebApplicationBuilder builder)
    {
        var uriSecret = builder.Configuration.GetValue<string>("FrontUri:Secret");
        if ( string.IsNullOrEmpty(uriSecret))
        { 
            Console.WriteLine("o segredo está nulo");
        }
        else
        {
            builder.Services.AddCors(x => x.AddPolicy(
                "Allow", policyBuilder =>
                    policyBuilder.WithOrigins(uriSecret).AllowAnyHeader().AllowAnyMethod()));
        }
        
    }
}