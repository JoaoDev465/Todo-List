using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using IConfiguration = Microsoft.Testing.Platform.Configurations.IConfiguration;

namespace TodoListTest.TestExtensiveServices;


public class TestExtensiveDbContextService
{
   
    [Fact]
    public async Task TestConnectionStringsFromSQL()
    {
        var builderToGetIconfigurationsConnectionStrings = WebApplication.CreateBuilder();

        var ConnectionStrings = builderToGetIconfigurationsConnectionStrings.Configuration.
            GetConnectionString("connection");

        var testConnectionStringOptions =
            new DbContextOptionsBuilder<DbContext>()
                .UseSqlServer(ConnectionStrings).Options;

        await using var ContextTestConnection =
            new DbContext(testConnectionStringOptions);
     
            var TestConnectionStrings = ContextTestConnection.Database.CanConnectAsync();
            Assert.IsNotNull(TestConnectionStrings);
            Assert.IsTrue(TestConnectionStrings.Result,"conectado com sucesso");
            
        
    }
}