using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.DeleteHandler;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;

public class TestDeleteTaskHandler
{
     [Fact]
     public async Task TestDeleteHandler_Assert_When_GenerericRequest_ReturnNull()
     {
          var options = new DbContextOptionsBuilder<Context>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

          var context = new Context(options);

          context.Todos.Add(new Todo
          {
               Id = 1,
               Task = "ir ao mercado"
          });

         await context.SaveChangesAsync();

         var handler = new DeleteTaskHandler(context);
         var deleteContent = new TodoDto
         {
              Id = 1,
              Task = "ir ao mercado"
         };

         var resultContentDeleted = await handler.DeleteAsync(deleteContent);
          var save =   await context.SaveChangesAsync();
         
         await context.Todos.FirstOrDefaultAsync(x => x.Id == 1);
        Xunit.Assert.True(save == 0);
        Xunit.Assert.Equal(200,resultContentDeleted.Code);
         
     }

     [Fact]
     public async Task Test_HandlerDelete_When_IdIsNotSame()
     {
          var options = new DbContextOptionsBuilder<Context>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

          var context = new Context(options);

          context.Todos.Add(new Todo
          {
               Id = 1,
               Task = "ir ao mercado"
          });

          await context.SaveChangesAsync();

          var handler = new DeleteTaskHandler(context);
          var deleteContent = new TodoDto
          {
               Id = 0
              
          };

          var resultContentIsNotDeleted = await handler.DeleteAsync(deleteContent);
          
          Xunit.Assert.Equal(404,resultContentIsNotDeleted.Code);
     }
}