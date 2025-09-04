using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Handlers.PutHandlers;
using TodoListCore.Models;
using TodoListCore.Uses_Cases.DTO;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace TodoListTest.UniTests.TestHandlers.TestTaskHandlers;


public class TestPutHAndler 
{
    [Fact]
    public async Task TestPutTAsks_When_IsValid()
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
      

        Xunit.Assert.Equal(task.Id, newtask.Data.Id);
        Xunit.Assert.NotNull(newtask);
        Xunit.Assert.Equal("atualizando",newtask.Data.Task);
        Xunit.Assert.Equal(200,newtask.Code);
        
    }
    [Fact]
    public async Task TestPutHandler_WhenData_Id_Is_not_Same_That_Initial_Request()
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
    
        var newtask=  await  handler.PutAsync(id:0,new TodoDto
        {
            Id = 0,
            Task = "atualizando",
            DescriptionOfTask = "testes"
        });
        
        Xunit.Assert.Equal(404,newtask.Code);

    }
    
    
}