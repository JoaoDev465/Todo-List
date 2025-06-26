using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/user/{id:int}")]

public class GetTaskController:ControllerBase
{
    private readonly Context _context;

    public GetTaskController(Context context)
    {
        _context = context;
    }
   
    [HttpGet]
    public async Task<IActionResult> Get_OneUser(
        [FromRoute] int id)
    {
        var user = await _context.Users
            .Where(x => x.Id == id).
            Select(x => new UserDto()
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