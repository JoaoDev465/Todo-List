using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.TestControllers.TestGetController;

public class TestGetOneUser: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _helper;

    public TestGetOneUser(WebApplicationFactory<Program> factory,
        ITestOutputHelper helper)
    {
        _client = factory.CreateClient();
        _helper = helper;
    }

    [Fact]
    public async Task TestReturnOneClientInGetController()
    {
        int id = 12;
      var USer =  await _client.GetAsync($"api/v1/user/{id}");
      
      _helper.WriteLine(USer.ReasonPhrase);
      _helper.WriteLine(USer.Content.ReadAsStringAsync().Result);
     
      
      Assert.IsNotNull(USer);
        
    }
}