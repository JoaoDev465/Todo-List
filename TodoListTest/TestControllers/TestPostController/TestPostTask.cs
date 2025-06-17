using Microsoft.AspNetCore.Mvc.Testing;
using ViewModels.Todo;
using Xunit;
using Xunit.Sdk;

namespace TodoListTest.TestControllers.TestPostController;

public class TestPostTask: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly TestOutputHelper _helper;
    private readonly HttpClient _client;
   
    public TestPostTask(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task TestReturnOkFromPostTask()
    {
        var newpost = new ViewTodo()
        {
            InitializeDateTimeTask = DateTime.Now,
            AlertForDateTask = DateTime.Now,
            DescriptionOfTask = "ir ao supermercado hoje",
            FinalizedTimeTask = false,
            Start_Task = false
        };
        
    }
}