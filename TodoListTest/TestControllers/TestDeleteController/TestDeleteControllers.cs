using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using ViewModels.User;
using Xunit;
using Xunit.Abstractions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.TestControllers.TestDeleteController;

public class TestDeleteControllers: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _helper;

    public TestDeleteControllers(WebApplicationFactory<Program> factory,
        ITestOutputHelper helper)
    {
        _client = factory.CreateClient();
        _helper = helper;
    }

    [Fact]
    public async Task TestReturnDeletedInDeleteController()
    {
        int id = 12;
        var USerHttpDelete =
            _client.DeleteAsync($"v1/Delete/user/{id}");
        
        _helper.WriteLine(USerHttpDelete.Result.ReasonPhrase);
        _helper.WriteLine(USerHttpDelete.Result.Content.ToString());

        Assert.AreEqual(HttpStatusCode.OK, USerHttpDelete.Result.StatusCode);
    }
}