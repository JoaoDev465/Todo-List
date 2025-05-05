using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
public class GetController:ControllerBase
{
    [HttpGet("v1/user/{id:int}")]
    public async Task<IActionResult> Get_OneUser(
      [FromServices] Context context ,
        [FromRoute] int id)
    {
        var user = await context.Users
            .Where(x => x.Id == id).
            Select(x => new ViewDataUser()
            {
                Id = x.Id,
                UserEmail = x.Email,
                UserName = x.Name,
                UserPassword = x.PasswordHash
            }).FirstOrDefaultAsync();

        if (user == null)
            return NotFound(
                new ResultViewsDataAndErrorsInJSON<string>
                    ("usuário não encontrado"));

        return Ok(user);
    }
}