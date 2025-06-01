using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveTokenService
{
    public static void TokenService(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IGenerateTokenService,GenerateTokenService>();
    }

}