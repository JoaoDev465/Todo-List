using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TodoList.Proj;
using TodoListTest.TestControllers.TestGetController;
using ViewModels.Role;
using ViewModels.User;
using Xunit;
using Xunit.Abstractions;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JsonSerializer = Microsoft.ApplicationInsights.Extensibility.Implementation.JsonSerializer;

namespace TodoListTest.TestControllers.TestPostController;

public class TestPostUser: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _helper;

    public TestPostUser(WebApplicationFactory<Program>factory,
        ITestOutputHelper helper)
    {
        _client = factory.CreateClient();
        _helper = helper;
    }

    [Fact]
    public async Task TestReturnContentInPostUser()
    {
        var usercontent = new UserDto
        {
            Id = 1,
            UserName = "joãobananabonito",
            UserEmail = "joaodesouza@gmail.com",
            UserPassword = "JoaoDark1980@3$1010",
            Slug = "joao-Marcelo-lindo"
        };

        var Json = System.Text.Json.JsonSerializer.Serialize(usercontent);
        var content = new StringContent(Json, Encoding.UTF8, "application/json");

        try
        {
            
            var response = await _client.PostAsync("api/v1/user", content);
            response.EnsureSuccessStatusCode();

            var responseJson = await content.ReadAsStringAsync();
            var taskCreated = System.Text.Json.JsonSerializer.Deserialize<UserDto>(
                responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            
        
            _helper.WriteLine(response.ReasonPhrase);
            _helper.WriteLine(response.Content.ToString());
            _helper.WriteLine(responseJson);
            
            Assert.IsNotNull(taskCreated);
            Assert.AreEqual("joãobananabonito", usercontent.UserName);
            
            

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}