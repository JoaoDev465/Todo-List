using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using TodoList.Proj.Models;
using TodoList.Proj.Services.TokenService;
using TodoListCore.Interfaces;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestServices;

public class TestUnitGenerateTokenServices
{
    private readonly ITestOutputHelper _testOutputHelper;
  

    public TestUnitGenerateTokenServices(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestGenerateToken_whenGenericTokenIS_Valid()
    {
        var user = new User
        {
            Name = "Pastel"
        };
        var InMemorySettings = new Dictionary<string, string>()
        {
            { "JwtSettings:secret", "8236tqyewdodqijooiqwjnqljdqwhduoiqkqheq" }
        };

        IConfiguration configuration = new ConfigurationManager()
            .AddInMemoryCollection(InMemorySettings).Build();
        
        var Service = new GenerateTokenService(configuration);

        var token = Service.TokenGenerator(user);
        _testOutputHelper.WriteLine(token);

        Assert.NotNull(token);
        
    }
}