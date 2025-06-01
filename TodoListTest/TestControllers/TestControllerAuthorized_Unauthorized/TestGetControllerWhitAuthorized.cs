using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Proj.Services.TokenService;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.TestControllers.TestGetController;

public class TestGetControllerWhitAuthorized:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private ITestOutputHelper _ItestOutputHelper;

    public TestGetControllerWhitAuthorized(WebApplicationFactory<Program>Factory,
        ITestOutputHelper itestOutputHelper, WebApplicationFactory<Program> factory)
    {
        _ItestOutputHelper = itestOutputHelper;
        _factory = factory;
    }

    [Theory]
    [InlineData("/v1/list/task")]
    public async Task TestController_StatusAuthorized_403(string url)
    {
        var client = _factory.CreateClient();
        var headerauthorization = client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TESTE");
        var responseHttpStatuscode = await client.GetAsync(url);
        
        _ItestOutputHelper.WriteLine($"{headerauthorization}:{responseHttpStatuscode}");

        Assert.NotNull(client);
        
    }
}