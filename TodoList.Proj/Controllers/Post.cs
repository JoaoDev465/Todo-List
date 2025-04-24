using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;
using TodoList.Proj.TokenGenerator;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers;


[ApiController]
public class PostController : ControllerBase
{
   

    [HttpPost("v1/user")]
    public async Task<IActionResult> Post_User(
        [FromServices] Context context,
        [FromBody] ViewUser Users)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViews<ViewUser>(ModelState.GetErrors()));

        User user = new User()
        {
            Name = Users.Name,
            Email = Users.Email,
            PasswordHash = Users.Password
            
        };

        try
        {
            await context.AddAsync(user);
            
            return Ok(new ResultViews<ViewUser>(Users));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
       
    }
}

