using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
public class GetListController: ControllerBase
{
    [HttpGet("v1/list/user")]
    public async Task<IActionResult> Get_List_User(
        [FromServices] Context context)
    {
        var users = await context.Users.Select(x => new User()
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            PasswordHash = x.PasswordHash,
            Roles = new List<Role>(),
            Todos = new List<Todo>()
        }).ToListAsync();

        return Ok(new ResultViewsDataAndErrorsInJSON<List<User>>(users));
    }
}