using Microsoft.EntityFrameworkCore;
using Moq;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PostHandler;
using TodoListCore.ControllersHandlers;
using ViewModels.Todo;
using Xunit;
using Assert = Xunit.Assert;

namespace TodoListTest.MockTests.TestTaskHandlers;

public class TaskHandlerCreate
{

    [Fact]
    public async Task TestTaskCreate_When_Content_Is_Created()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: "TODO")
            .Options;

        var context = new Context(options);

        var handler = new TaskhandlerCreate(context);
        var request = new TodoDTO
        {
            Task = "Ir ao Supermercado"
        };

        var result = await handler.CreateAsync(request);

        Assert.NotNull(result);
        Assert.Equal("Ir ao Supermercado",request.Task);
    }

}