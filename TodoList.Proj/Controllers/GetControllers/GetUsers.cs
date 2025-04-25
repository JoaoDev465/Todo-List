using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]

public class GetController:ControllerBase
{
    [HttpGet("v1/GetOne/{id}")]
    public async Task<IActionResult> GetOneUser(
        [FromRoute] int id,
        [FromServices] Context context)
    {
        try
        {

            var OneUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (OneUser == null)
                return NotFound(new ResultViews<User>(OneUser));
            else
            {
                return Ok(new ResultViews<User>(OneUser));
            }
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetListUsers(
        [FromServices] Context context)
    {
        var Userlist = await context.Users.ToListAsync();

        try
        {
            if (Userlist == null)
                return NotFound(new ResultViews<List<User>>(Userlist));
            else
            {
                return Ok(new List<User>());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}