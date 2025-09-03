using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoList.Proj.Data;
using Xunit;

namespace TodoListTest.UniTests.DbTest;

public class TestDbConnection
{
    [Fact]
    public async Task DbConnectionTest()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.tests.json").Build();
        
        var connection = config.GetConnectionString("connection");
        var options = new DbContextOptionsBuilder<Context>().UseSqlServer(connection).Options;
       using var context = new Context(options);
       var testconnection = await context.Database.CanConnectAsync();
       
       Xunit.Assert.True(testconnection,"falha na conexão");

    }
}