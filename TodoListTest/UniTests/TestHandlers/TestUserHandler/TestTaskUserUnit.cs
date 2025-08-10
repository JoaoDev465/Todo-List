using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PutHandlers;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestUserHandler;

public class TestHandlerUserUnit
{
    [Fact]
    public async Task TestHAndlerUpdate_UserUpdate_WhenRequestIsValid()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var handler = new PutUserHandler(context,new PasswordHasher<User?>());

        await context.Users.AddAsync(new User
        {
            Id = 1,
            Email = "joao1234@gmail.com",
            PasswordHash = "olaDark1234@#"
        });

        await context.SaveChangesAsync();

        var request = new UserDto
        {
            Id = 1,
            UserEmail = "joaoBaia@gmail.com",
            UserPassword = "Dark1234@gmail.com"
        };

       var result = await handler.PutAsync(request);

       Assert.NotNull(result);
       Assert.NotNull(result.Data);
       Assert.Equal("joaoBaia@gmail.com",result.Data.Email);
    }
}