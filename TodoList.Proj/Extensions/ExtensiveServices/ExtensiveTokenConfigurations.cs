using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace TodoList.Proj.Extensions.ExtensiveServices;

public static class ExtensiveTokenConfigurations
{
    public static void TokenServiceConfiguration(this WebApplicationBuilder builder)
    {
        var secret = builder.Configuration["JwtSettings:secret"];
        var key = Encoding.UTF8.GetBytes(secret);
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true
            };
            x.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("authentication failed" + context.Exception.Message);
                    return Task.CompletedTask;
                }
            };
        });
        builder.Services.AddAuthorization(c =>
        {
            c.AddPolicy("user", policy =>
                policy.RequireAuthenticatedUser());
        });

    }

}