using System.Runtime.InteropServices.JavaScript;
using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.PostControllers;

[ApiController]
public class PostTodoController: ControllerBase
{
    private readonly Context _context;
    public PostTodoController(Context context)
    {
        _context = context;
    }
   
    [HttpPost("v1/post/task")]
    public async Task<IActionResult> Post_Tasks(
        [FromBody] ViewTodo todo)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewsDataAndErrorsInJSON
                <string>(ModelState.GetErrors()));

        var userTodo = new Todo
        {
            Start = todo.Start_Task,
            Initialized = DateTime.Now,
            Task = todo.Task,
            Description = todo.DescriptionOfTask,
            Alert = DateTime.Now,
            Finalized = todo.FinalizedTimeTask,
            UserId = todo.userId
        };

        try
        {
            await _context.AddAsync(userTodo);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewsDataAndErrorsInJSON
                <string>("falha interna no servidor"));
        }

        return Ok(new ResultViewsDataAndErrorsInJSON<Todo>(userTodo));
    }
}