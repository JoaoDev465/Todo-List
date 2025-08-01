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
    private readonly string? _configuration;

    public TestUnitGenerateTokenServices(ITestOutputHelper testOutputHelper,
        IConfiguration conf)
    {
        _testOutputHelper = testOutputHelper;
        _configuration = conf["JwtSettings:secret"];
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