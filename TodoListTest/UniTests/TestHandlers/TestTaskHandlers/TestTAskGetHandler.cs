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
    public async Task Test_IsValid_When_ReturGenericRequest()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString() ).Options;

        var context = new Context(options);

        var handler = new TaskHandlerGet(context);

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

        var result =  await handler.GetByIdAsync(request);
        
        Assert.IsNotNull(result);
    }
}