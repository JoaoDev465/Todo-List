using System.IdentityModel.Tokens.Jwt;
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
        var Service = new GenerateTokenService("abc1234");

        var token = Service.TokenGenerator(user);
        _testOutputHelper.WriteLine(token);

        Assert.NotNull(token);
        
    }
}