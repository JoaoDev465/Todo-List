using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PutHandlers;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Xunit.Abstractions;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;


public class TestPutHAndler 
{
    private readonly ITestOutputHelper _testOutputHelper;
    public TestPutHAndler(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    [Fact]
    public async Task TestPutTAsks()
    {
        var options = new DbContextOptionsBuilder<Context>().
            UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

        var context = new Context(options);

        var handler = new PutTaskHandler(context);

        var task = new Todo
        {
            Id = 1,
            Task = "ir ao mercado",
            Description = "ir no mercado mig hoje"
        };

        context.Add(task);
        context.SaveChanges();
    
     var newtask=  await  handler.PutAsync(task.Id,new TodoDto
        {
            Id = 1,
            Task = "atualizando",
            DescriptionOfTask = "testes"
        });
        _testOutputHelper.WriteLine(newtask.Data.ToString());

        Xunit.Assert.Equal(task.Id, newtask.Data.Id);
        Xunit.Assert.NotNull(newtask);
        Xunit.Assert.Equal("atualizando",newtask.Data.Task);
        
    }
}