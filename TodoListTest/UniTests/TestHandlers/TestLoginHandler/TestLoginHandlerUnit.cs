using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.AuthHandlers;
using TodoList.Proj.Services.TokenService;
using TodoListCore;
using TodoListCore.Interfaces;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace TodoListTest.UniTests.TestHandlers.TestLoginHandler;

public class TestLoginHandlerUnit 
{

    [Fact]
    public async Task TestHandle_WhenUSerRequest_IsNotNull()
    {
        var hash = new PasswordHasher<User>();
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new Context(options);

        var inMemorySettings = new Dictionary<string, string>()
        {
            { "JwtSettings:secret", "983279y9872yhudkhqbdkjasbmaslxqljdkoqmoq" }
        };
        IConfigurationRoot configuration = new ConfigurationManager()
            .AddInMemoryCollection(inMemorySettings).Build();
        
        var handler = new LoginHandler(context,new GenerateTokenService(configuration),hash);
        var request = new LoginDTO
        {
            UserId = 1,
            UserEmail = "joao@gmail.com",
            UserPassword = "Dark1234@ola"
        };
       await   context.Users.AddAsync(new User
        {
            Id = 1,
            Email = "joao@gmail.com",
            PasswordHash = hash.HashPassword(null,request.UserPassword)
        });

        await context.SaveChangesAsync();
        var result = await handler.LoginAsync(request);

        Assert.NotNull(result);
    }
}