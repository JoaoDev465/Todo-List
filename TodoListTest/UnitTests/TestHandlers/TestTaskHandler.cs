using Microsoft.AspNetCore.Mvc;
using TodoListCore.ControllersHandlers;
using TodoListCore.IHandlers.IDeleteHandlers;
using TodoListCore.IHandlers.IGetHandler;
using TodoListCore.IHandlers.IPutHandler;
using ViewModels.Todo;
using Xunit;
using Assert = Xunit.Assert;

namespace TodoListTest.UnitTests.TestHandlers;

[TestClass]
public class UnitTestTaskHandlerCreate
{
    private readonly ITaskHandlerCreate _taskHandlerCreate;
    
    public UnitTestTaskHandlerCreate(ITaskHandlerCreate create)
    {
        _taskHandlerCreate = create;
       
    }

    [Fact]
    public void TestTaskHandlerCreate_ShouldReturnsSuccesses_WhenTask_IS_Valida()
    {
      var createContentToTasks =  _taskHandlerCreate.CreateAsync(new TodoDTO
      {
          Task = "ir ao mercado"
      });

      var result = createContentToTasks.Result.IsSuccess;
      
      Assert.True(result);
      Assert.NotNull(createContentToTasks);

    }
}