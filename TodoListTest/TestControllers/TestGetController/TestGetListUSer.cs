using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.TestControllers.TestGetController;

public class TestGetListUSer:IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _itestOutputHelper;
    private readonly StringContent _content;

    public TestGetListUSer(WebApplicationFactory<Program>factory,
        ITestOutputHelper itesOutputHelper
        )
    {
       
        _factory = factory;
        _itestOutputHelper = itesOutputHelper;
    }
    
    [Fact]
    public async Task TestGetController_IfReturnContent()
    {
        var httpClient = _factory
            .CreateClient().GetAsync("v1/list/user");
        
        var content = await httpClient.
            Result.Content.ReadAsStringAsync();
        
        _itestOutputHelper.WriteLine($"conteúdo : {content}");

        Assert.NotNull(httpClient);
    }
}