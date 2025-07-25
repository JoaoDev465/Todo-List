using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.AuthHandlers;
using TodoList.Proj.Models;
using TodoList.Proj.Services.TokenService;
using TodoListCore;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using View.ViewModels;
using ViewModels.User;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestLoginHandler;

public class TestLoginHandlerUnit 
{

    [Fact]
    public async Task TestHandle_WhenUSerRequest_IsNotNull()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new Context(options);
        
        var handler = new LoginHandler(context, new GenerateTokenService(new JwtSecurityTokenHandler()));
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
            PasswordHash = "Dark1234@ola"
        });

        await context.SaveChangesAsync();
        var result = await handler.LoginAsync(request);

        Assert.NotNull(result);
    }
}