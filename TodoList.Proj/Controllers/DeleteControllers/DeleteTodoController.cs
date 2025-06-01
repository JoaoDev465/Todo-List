using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.DeleteControllers;

public class DeleteTodoController : ControllerBase
{
    [Authorize]
    [HttpDelete("v1/Delete/Todos/{id:int}")]
    public async Task<IActionResult> DeleteTodos(
        [FromServices] Context context,
        [FromRoute] int id )
    {
        var task = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

        if (task.Id == null)
        {
            return StatusCode(404,
                new ResultViewsDataAndErrorsInJSON<User>
                    ("tarefa não encontrada"));
        }

        try
        {
            context.Remove(task);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok($"Usuário de Id {task.Id} removido com sucesso");
    }
}