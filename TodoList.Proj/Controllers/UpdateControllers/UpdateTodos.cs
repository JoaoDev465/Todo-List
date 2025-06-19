using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.UpdateControllers;

public class UpdateTodosController : ControllerBase
{
    private readonly Context _context;
    public UpdateTodosController(Context context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpPut("v1/update/Todos/{id:int}")]
    public async Task<IActionResult> UpdateTodos(
        [FromRoute] int id,
        [FromBody] TodoDTO todoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewsDataAndErrorsInJSON
                <string>(ModelState.GetErrors()));
      
        var tasks = await _context.Todos
            .FirstOrDefaultAsync(x => x.Id == id);

        if (tasks == null)
        {
            return NotFound(new ResultViewsDataAndErrorsInJSON
                <Todo>(tasks));
        }

        tasks.Start = todoDto.Start_Task;
        tasks.Initialized = todoDto.InitializeDateTimeTask;
        tasks.Task = todoDto.Task;
        tasks.Description = todoDto.DescriptionOfTask;
        tasks.Alert = todoDto.AlertForDateTask;
        tasks.Finalized = todoDto.FinalizedTimeTask;

        try
        {
            _context.Update(tasks);
            await _context.SaveChangesAsync();
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