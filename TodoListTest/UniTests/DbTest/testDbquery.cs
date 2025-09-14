using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoListCore.Models;
using Xunit;

namespace TodoListTest.UniTests.DbTest;

public class TestDbquery
{
    [Fact]
    public async Task TestDbqueryFromContext()
    {
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var todo = new Todo
        {
            Task = "testando",
            Description = "isso é um teste"
        };

       var query = await context.AddAsync(todo);
     var resulltFromquery =  await context.SaveChangesAsync();
       
       Xunit.Assert.True(resulltFromquery> 0, "não há dados salvos");
    }
}
