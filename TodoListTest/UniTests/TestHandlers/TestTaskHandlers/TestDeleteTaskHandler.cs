using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.DeleteHandler;
using TodoList.Proj.Models;
using ViewModels.Todo;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.UniTests.TestTaskHandlers;

public class TestDeleteTaskHandler
{
     [Fact]
     public async Task TestDeleteHandler_Assert_When_GenerericData_ReturnNull()
     {
          var options = new DbContextOptionsBuilder<Context>()
               .UseInMemoryDatabase(databaseName: "TODO").Options;

          var context = new Context(options);

          context.Todos.Add(new Todo
          {
               Id = 1,
               Task = "ir ao mercado"
          });

         await context.SaveChangesAsync();

         var handler = new DeleteTaskHandler(context);
         var deleteContent = new TodoDTO
         {
              Id = 1,
              Task = "ir ao mercado"
         };

         var resultContentDeleted = await handler.DeleteAsync(deleteContent);
         await context.SaveChangesAsync();
         
         await context.Todos.FirstOrDefaultAsync(x => x.Id == 1);
         Assert.IsNull(resultContentDeleted);
         
     }
}