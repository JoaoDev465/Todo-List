using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.GetHandler;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;

public class TestTAskGetHandler
{
    [Fact]
    public async Task Test_IsValid_When_ReturGenericRequestIs_200()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() ).Options;
        var acessor = new HttpContextAccessor();
        var context = new Context(options);

        var handler = new TaskHandlerGet(context, acessor);

        context.Todos.Add(new Todo
        {
            Id = 1,
            Task = "ir ao supermercado"
        });
        await context.SaveChangesAsync();
        
        var request = new TodoDto
        {
            Id = 1
        };

        var result =  await handler.GetByIdAsync(request.Id);
        
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(200,result.Code);
    }

    [Fact]
    public async Task TestGetHandler_When_IdIsNull()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() ).Options;

        var context = new Context(options);
        var acessor = new HttpContextAccessor();

        var handler = new TaskHandlerGet(context,acessor);

        context.Todos.Add(new Todo
        {
            Id = 1,
            Task = "ir ao supermercado"
        });
        await context.SaveChangesAsync();
        
        var request = new TodoDto
        {
            Id = 0
        };

        var result =  await handler.GetByIdAsync(request.Id);
        
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(404,result.Code);
    }
}