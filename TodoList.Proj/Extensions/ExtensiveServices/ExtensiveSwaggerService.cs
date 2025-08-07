using Microsoft.OpenApi.Models;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveSwaggerService
{
    public static void SwaggerApplicationService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {

            var securityScheme = new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Digite o seu token JWt no campo abaixo"
            };
            
            x.AddSecurityDefinition("bearer",securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new[] { "bearer" }
                }
            };
            
            x.AddSecurityRequirement(securityRequirement);
        });


    }

}