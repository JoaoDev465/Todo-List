using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.DeleteControllers;


[ApiController]
public class DeleteController : ControllerBase
{
    [Authorize]
    [HttpDelete("v1/Delete/user/{id:int}")]
    public async Task<IActionResult> DeleteUSers(
        [FromServices] Context context,
        [FromRoute] int id )
    {
        var user = await context.
            Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user.Id == null)
        {
            return StatusCode(404,
                new ResultViewsDataAndErrorsInJSON<User>
                    ("usuário não encontrado"));
        }

        try
        {
            context.Remove(user);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok($"Usuário de Id {user.Id} removido com sucesso");
    }
}