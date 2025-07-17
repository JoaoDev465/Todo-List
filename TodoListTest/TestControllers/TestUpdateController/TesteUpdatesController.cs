using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Proj;
using TodoListTest.TestControllers.TestGetController;
using ViewModels.User;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.TestControllers.TestUpdateController;

public class TestUpdatesController: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _helper;

    public TestUpdatesController(WebApplicationFactory<Program> factory,
        ITestOutputHelper helper)
    {
        _helper = helper;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task TestReturnUserUpdatedInResponse()
    {
        int id = 13;
        var newuser = new UserDto
        {
            UserName = "joaobonitolindo",
            UserEmail = "joaobaia@gmail.com",
            UserPassword = "dark1980$$OLA1234"
        };

        var json = JsonSerializer.Serialize(newuser);
        var contentHttp = new StringContent(json, Encoding.UTF8, "application/json");
        
        var UserHttpResponse = await _client.PutAsync($"api/v1/user/{id}",contentHttp);
        UserHttpResponse.EnsureSuccessStatusCode();

        var responseJson =await  UserHttpResponse.Content.ReadAsStringAsync();
        var TaskStringJsonContent = JsonSerializer.Deserialize<UserDto>
        (responseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        _helper.WriteLine($"status code {(int)UserHttpResponse.StatusCode } ");
        _helper.WriteLine(responseJson);
        _helper.WriteLine(UserHttpResponse.ReasonPhrase);
        _helper.WriteLine(contentHttp.ToString());

        Assert.NotNull(TaskStringJsonContent);
        Xunit.Assert.Equal("joaobonitolindo",newuser.UserName);
    }
}