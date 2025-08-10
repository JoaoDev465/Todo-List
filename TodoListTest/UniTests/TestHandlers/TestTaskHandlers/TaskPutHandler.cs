using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PutHandlers;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;

public class TestTaskPutHandler
{
    [Fact]
    public async Task TestHAndlerPut_Assert_When_GenericRequestIsUpdated()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var handler = new PutTaskHandler(context);

        context.Todos.Add(new Todo
        {
            Id = 1,
            Task = "ir ao mercado"
        });
      await   context.SaveChangesAsync();

      var updateRequest = new TodoDto
      {
          Task = "jogar bola"
      };

      await handler.PutAsync(updateRequest);
      await context.SaveChangesAsync();

     await  context.Todos.FirstOrDefaultAsync(x => x.Id == 1);
     Assert.AreEqual("jogar bola", updateRequest.Task);

    }
}