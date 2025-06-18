using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Proj.Models;
using ViewModels.Todo;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Assert = Xunit.Assert;

namespace TodoListTest.TestControllers.TestPostController;

public class TestPostTask : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _helper;
    private readonly HttpClient _client;

    public TestPostTask(WebApplicationFactory<Program> factory,
        ITestOutputHelper helper)
    {
        _client = factory.CreateClient();
        _helper = helper;
    }

    [Fact]
    public async Task TestReturnOkFromPostTask()
    {
        var newpost = new ViewTodo
        {
            InitializeDateTimeTask = DateTime.Now,
            AlertForDateTask = DateTime.Now,
            DescriptionOfTask = "ir ao supermercado hoje pela manhã e comprar azeite",
            FinalizedTimeTask = false,
            Start_Task = false,
            Task = "ir ao supermercado",
            userId = 12
        };

        var json = JsonSerializer.Serialize(newpost);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("V1/post/task", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        var taskCreated = JsonSerializer.Deserialize<ViewTodo>
        (responseJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        
        _helper.WriteLine($"status code {(int)response.StatusCode } ");
        _helper.WriteLine(responseJson);
        _helper.WriteLine(response.ReasonPhrase);
        _helper.WriteLine(content.ToString());
        
        Assert.NotNull(taskCreated);
        Assert.Equal("ir ao supermercado", newpost.Task);
        Assert.True(taskCreated.userId == 0);
        

    }


}