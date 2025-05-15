using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.UpdateControllers;

public class UpdateTodosController : ControllerBase
{
    [HttpPut("v1/update/user/{id:int}")]
    public async Task<IActionResult> UpdateUser(
        [FromServices] Context context,
        [FromRoute] int id,
        [FromBody] ViewTodo todo)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewsDataAndErrorsInJSON
                <string>(ModelState.GetErrors()));
      
        var tasks = await context.Todos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tasks == null)
        {
            return NotFound(new ResultViewsDataAndErrorsInJSON
                <Todo>(tasks));
        }

        tasks.Start = todo.Start_Task;
        tasks.Initialized = todo.InitializeDateTimeTask;
        tasks.Task = todo.Task;
        tasks.Description = todo.DescriptionOfTask;
        tasks.Alert = todo.AlertForDateTask;
        tasks.Finalized = todo.FinalizedTimeTask;

        try
        {
            context.Update(tasks);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewsDataAndErrorsInJSON
                <Todo>(tasks));
        }

        return Ok(new ResultViewsDataAndErrorsInJSON
            <Todo>(tasks));
    }
}