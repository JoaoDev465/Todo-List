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
        var password = hash.HashPassword(null, request.UserPassword);
       await   context.Users.AddAsync(new User
        {
            Id = 1,
            Email = "joao@gmail.com",
            PasswordHash = password
        });

     var save =   await context.SaveChangesAsync();
        var result = await handler.LoginAsync(request);

        Assert.NotNull(result);
        Assert.True(save > 0);
        Assert.Equal(200,result.Code);
    }

    [Fact]
    public async Task TestHAndler_WhenDatasRequired_IsNull()
    {
        var inMemorySettings = new Dictionary<string, string>()
        {
            { "JwtSettings:secret", "983279y9872yhudkhqbdkjasbmaslxqljdkoqmoq" }
        };
        IConfigurationRoot configuration = new ConfigurationManager()
            .AddInMemoryCollection(inMemorySettings).Build();
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        
        var context = new Context(options);
        var token = new GenerateTokenService(configuration);
        var hash = new PasswordHasher<User> ();

        var request = new LoginDTO()
        {
            UserId = 0,
            UserEmail = null,
            UserPassword = null
        };
        var handler = new LoginHandler(context, token, hash);

        var result = await handler.LoginAsync(request);

      var save =  await context.SaveChangesAsync();
      
      Xunit.Assert.True(save == 0, "nenhum usuário encontrado");
      Xunit.Assert.Equal("nenhum usuário com essa senha foi encontrado",result.Message);
      
    }
    
}
