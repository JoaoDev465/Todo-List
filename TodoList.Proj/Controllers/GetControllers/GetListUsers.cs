using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using ViewModels.ResultViews;

namespace TodoList.Proj.Controllers.GetControllers;
[Route("/v1/list/user")]
[ApiController]
public class GetListUserController: ControllerBase
{
    private readonly Context _context;
    public GetListUserController(Context context)
    {
        _context = context;
    }
    
    [HttpGet("/v1/list/user")]
    public async Task<IActionResult> Get_List_User(
        [FromQuery] int page )
    {
        
        var users = await _context.Users.Select(x => new User()
        {
            Id = x.Id,
            Name = x.Name,
            Email = x.Email,
            PasswordHash = x.PasswordHash,
            Roles = new List<Role>(),
            Todos = new List<Todo>()
        }).ToListAsync();

        return Ok(new ResultViewsDataAndErrorsInJSON
            <List<User>>(users));
    }
}