using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.DeleteControllers;

public class DeleteTodoController : ControllerBase
{
    private readonly Context _context;

    public DeleteTodoController(Context context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpDelete("v1/Delete/Todos/{id:int}")]
    public async Task<IActionResult> DeleteTodos(
        [FromRoute] int id )
    {
        var task = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (task.Id == null)
        {
            return StatusCode(404,
                new ResultViewsDataAndErrorsInJSON<User>
                    ("tarefa não encontrada"));
        }

        try
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok($"Usuário de Id {task.Id} removido com sucesso");
    }
}