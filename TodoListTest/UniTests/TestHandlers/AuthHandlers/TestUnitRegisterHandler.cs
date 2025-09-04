using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.AuthHandlers;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestRegisterHandler;

public class TestUnitRegisterHandler
{
    private readonly ITestOutputHelper _helper;

    public TestUnitRegisterHandler(ITestOutputHelper testOutputHelper)
    {
        _helper = testOutputHelper;
    }

    [Fact]
    public async Task TestRegister_When_RequestIsNotNull()
    {
        
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var handler = new RegisterHandler(context, new PasswordHasher<User?>());

        var request = new UserDto
        {
            Id = 1,
            UserEmail = "joaodesouza@gmail.com",
            UserPassword = "dark1234",
            Slug = "User-User"
        };
        
       var result = await handler.RegisterAsync(request);
      var save = await context.SaveChangesAsync();
       
       _helper.WriteLine($"{context}" == null ? "DbContext é null": "Dbcontext Ok");   
       
       Assert.Equal("joaodesouza@gmail.com",request.UserEmail);
       Assert.NotNull(result);
       Assert.NotNull(result.Data);
       Assert.Equal(201,result.Code);

    }

    [Fact]
    public async Task TestRegister_When_Data_Is_Same()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var handler = new RegisterHandler(context, new PasswordHasher<User?>());

        var requestuserone = new UserDto
        {
            Id = 1,
            UserEmail = "joao@gmail.com",
            UserPassword = "banana",
            Slug = "user-user"
        };
        var requestusertwo = new UserDto
        {
            Id = 1,
            UserEmail = "joao@gmail.com",
            UserPassword = "banana",
            Slug = "user-user"
        };
        
        var resultUserOne = await handler.RegisterAsync(requestuserone);
        var resultUserTwo = await handler.RegisterAsync(requestusertwo);
       var save = await context.SaveChangesAsync();
       
        _helper.WriteLine($"{context}" == null ? "DbContext é null": "Dbcontext Ok");   
        
       Assert.Equal("usuário já,existente",resultUserTwo.Message);
       Assert.Equal(400,resultUserTwo.Code);
    }
}