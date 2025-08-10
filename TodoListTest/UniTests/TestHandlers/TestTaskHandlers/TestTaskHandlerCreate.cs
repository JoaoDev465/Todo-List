using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PostHandler;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;

public class TestTaskHandlerCreate
{

    [Fact]
    public async Task TestTaskCreate_When_Request_Is_Valid()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new Context(options);

        var handler = new TaskhandlerCreate(context,new HttpContextAccessor());
        var request = new TodoDto
        {
            Task = "Ir ao Supermercado"
        };

        var result = await handler.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal("Ir ao Supermercado",request.Task);
    }

}