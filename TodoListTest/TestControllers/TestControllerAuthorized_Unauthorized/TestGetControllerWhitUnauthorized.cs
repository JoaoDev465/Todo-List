using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace TodoListTest.TestControllers.TestGetController;

public class TestGetControllerWhitUnauthorized:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;
    private string _url;
    private ITestOutputHelper _testOutputHelper; 
    
    public TestGetControllerWhitUnauthorized(WebApplicationFactory<Program> factory,
        ITestOutputHelper testOutputHelper,
        string url = "api/v1/task")
    {
        _factory = factory;
        _httpClient = new HttpClient();
        _url = url;
        _testOutputHelper = testOutputHelper;
    }


    [Theory]
    [InlineData("api/v1/task")]
    public async Task TestGetControllerWhitResult_401(string url)
    {
        var client = _factory.CreateClient();
        var responseHttpStatusCode = await client.GetAsync(url);

        _testOutputHelper.WriteLine($"{responseHttpStatusCode.StatusCode}");

        Xunit.Assert.NotNull(responseHttpStatusCode);
        Xunit.Assert.Contains("Forbidden", responseHttpStatusCode.ToString());

    }
}