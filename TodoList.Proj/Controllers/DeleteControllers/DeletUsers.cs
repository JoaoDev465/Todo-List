using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.DeleteControllers;


[ApiController]
public class DeleteUserController : ControllerBase
{
    private readonly Context _context;

    public DeleteUserController(Context context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpDelete("v1/Delete/user/{id:int}")]
    public async Task<IActionResult> DeleteUSers(
        [FromRoute] int id )
    {
        var user = await _context.
            Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user.Id == null)
        {
            return StatusCode(404,
                new ResultViewsDataAndErrorsInJSON<User>
                    ("usuário não encontrado"));
        }

        try
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return Ok($"Usuário de Id {user.Id} removido com sucesso");
    }
}