using System.IdentityModel.Tokens.Jwt;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveTokenService
{
    public static void TokenGenerateServicInteface(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IGenerateTokenService>();
    }
    public static void TokenService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<JwtSecurityTokenHandler>();
        builder.Services.AddSingleton<GenerateTokenService>();
    }

}