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
[Route("api/v1/task")]

public class PostTodoController: ControllerBase
{
    
    private readonly Context _context;
    public PostTodoController(Context context)
    {
        _context = context;
    }
   
    [HttpPost]
    public async Task<IActionResult> Post_Tasks(
        [FromBody] TodoDTO todoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewsDataAndErrorsInJSON
                <string>(ModelState.GetErrors()));

        var userTodo = new Todo
        {
            Start = todoDto.Start_Task,
            Initialized = DateTime.Now,
            Task = todoDto.Task,
            Description = todoDto.DescriptionOfTask,
            Alert = DateTime.Now,
            Finalized = todoDto.FinalizedTimeTask,
            UserId = todoDto.userId
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