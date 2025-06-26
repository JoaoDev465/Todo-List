using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.PostControllers;

[ApiController]
[Route("api/v1/user")]

public class UserController : ControllerBase
{
    private readonly Context _context;

    public UserController(Context context)
    {
        _context = context;
    }
  
    [HttpPost]
    public async Task<IActionResult> Post_User(
        [FromBody] UserDto users)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewsDataAndErrorsInJSON<UserDto>
                    (ModelState.GetErrors()));

        var user = new User
        {
            Name = users.UserName,
            Email = users.UserEmail,
            Slug = users.UserEmail.Replace("@","-").Replace(".","-")
           
        };
        
        user.PasswordHash = PasswordHasher.Hash(users.UserPassword);
        try
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new ResultViewsDataAndErrorsInJSON<dynamic>(new
            {
                users.UserName,
                users.UserEmail,
                users.UserPassword,
                user.Slug
            }));
        }
        catch (Exception e)
        {
            return StatusCode
            (500, new ResultViewsDataAndErrorsInJSON<string>
                ("falha interna no servidor"));
        }
      
    }

}