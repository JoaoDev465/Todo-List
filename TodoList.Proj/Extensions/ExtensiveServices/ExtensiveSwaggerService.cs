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
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Digite o token JWt no campo abaixo",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            
            x.AddSecurityDefinition("Bearer",securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    new string[] {}
                }
            };
            
            x.AddSecurityRequirement(securityRequirement);
        });


    }

}