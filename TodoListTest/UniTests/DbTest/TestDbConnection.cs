using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoList.Proj.Data;
using Xunit;

namespace TodoListTest.UniTests.DbTest;

public class TestDbConnection
{
    [Fact]
    public async Task DbConnectionTestFake()
    {
        
            
        var options = new DbContextOptionsBuilder<Context>()
            .UseInMemoryDatabase("banana").Options;
       using var context = new Context(options);
       var testconnection = await context.Database.CanConnectAsync();
       
       Xunit.Assert.True(testconnection,"falha na conexão");

    }
}