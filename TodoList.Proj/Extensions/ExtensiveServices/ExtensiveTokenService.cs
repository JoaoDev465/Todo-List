using System.IdentityModel.Tokens.Jwt;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Services.TokenService;
using TodoListCore.Interfaces;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveTokenService
{
  
    public static void TokenService(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<JwtSecurityTokenHandler>();
        builder.Services.AddTransient<IGenerateTokenService,GenerateTokenService>();
    }

}