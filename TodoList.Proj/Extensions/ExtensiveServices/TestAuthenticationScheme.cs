using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.ExtensionMethods;

public static class TestAuthenticationSchemeServices
{
    public static void TestAuthenticationEscheme(this WebApplicationBuilder builder)
    {
        builder.Services.RemoveAll(typeof(AuthenticationSchemeProvider));
        builder.Services.AddAuthentication().AddScheme<
            AuthenticationSchemeOptions, TestAuthHandler>("Test",options =>{});
    }

    public static void TestAuthenticationSchemeAddChallengeAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Test";
            options.DefaultChallengeScheme = "Test";
        });
    }
}