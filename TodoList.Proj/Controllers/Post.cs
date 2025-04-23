using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.TokenGenerator;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers;


[ApiController]
public class PostController : ControllerBase
{
    [HttpPost("v1/post/login")]
    public async Task<IActionResult> Post_Login(
        [FromServices] Context context,
        [FromServices] TokenService token
        )
    {
        var Token = token.GenerateToken(null);
        return Ok(Token);
    }

    [HttpPost("v1/user/login")]
    public async Task<IActionResult> Post_User(
        [FromServices] Context context,
        [FromBody] User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViews<User>(ModelState.GetErrors()));

        user = new User()
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash
            
        };

        try
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViews<User>(user));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
       
    }
}

