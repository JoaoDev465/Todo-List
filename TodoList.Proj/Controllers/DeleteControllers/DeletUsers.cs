using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.DeleteControllers;


[ApiController]
public class DeleteController : ControllerBase
{
    [HttpDelete("v1/Delete/user/{id}")]
    public async Task<IActionResult> DeleteUSers(
        [FromServices] Context context,
        [FromRoute] int id )
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user.Id == null)
        {
            return NotFound(new ResultViewsDataAndErrorsInJSON<User>(user));
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