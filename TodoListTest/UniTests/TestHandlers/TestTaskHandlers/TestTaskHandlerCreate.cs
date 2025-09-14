using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PostHandler;
using TodoListCore.Models;
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
        var dto = new TodoDto();
        var request = new Todo();

        var result = await handler.CreateAsync(new TodoDto
        {
            Task = dto.Task = "ir ao banheiro"
        });
        await context.AddAsync(request);
        var save = await context.SaveChangesAsync();
        Assert.NotNull(result);
        Assert.Equal("ir ao banheiro",dto.Task);
        Assert.True(save > 0);
    }

}