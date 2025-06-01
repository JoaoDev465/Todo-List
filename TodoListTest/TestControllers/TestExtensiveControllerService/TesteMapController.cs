using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.TestExtensiveServices.TestExtensiveControllerService;

public class TestMapController:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;
    private string Url;
    private HttpClient HttpClient;
    private readonly HttpContent _httpContent;
    
    public TestMapController(WebApplicationFactory<Program> factory, 
        ITestOutputHelper testOutputHelper,
        HttpContent content,
        string url = "/v1/list/user")
    {
        _httpContent = content;
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        Url = url;
        HttpClient = factory.CreateClient();
    }

    public async Task  GetOutPutHelperResponse_StatusCode()
    {
       
        var responseHttpStatusCode = await HttpClient.GetAsync(Url);
        _testOutputHelper.WriteLine($"status content:{responseHttpStatusCode.StatusCode}");
    }

    public async Task GetOutOutHelperResponse_Phrase()
    {
       
        var responseHttpReasonPhrase = await HttpClient.GetAsync(Url);
        _testOutputHelper.WriteLine($"Response Phrase : {responseHttpReasonPhrase.ReasonPhrase}");
    }

    public async Task GetHttpResponseToContent()
    {
        var responseHttp = await HttpClient.GetAsync(Url);
         await responseHttp.Content.ReadAsStreamAsync();
    }
    public async Task GetOutPutHelperResponse_Content()
    {
        await GetHttpResponseToContent();
        _testOutputHelper.WriteLine($" response content: {GetHttpResponseToContent()}");
    }
    
    [Theory]
    [InlineData("/v1/list/user")]
    public async Task GetMapResultSuccessfully(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);

        await GetOutPutHelperResponse_StatusCode();
        await GetOutOutHelperResponse_Phrase();
        await GetOutPutHelperResponse_Content();
        
        Xunit.Assert.NotNull(response);
        Xunit.Assert.True(response.IsSuccessStatusCode);
    }
}