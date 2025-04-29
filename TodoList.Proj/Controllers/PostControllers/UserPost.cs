using System.Data.Common;
using SecureIdentity.Password;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.Controllers.PostControllers;

using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;
using ViewModels.User;



[ApiController]
public class PostController : ControllerBase
{
   

    [HttpPost("v1/user")]
    public async Task<IActionResult> Post_User(
        [FromServices] Context context,
        [FromBody] ViewDataUser Users)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewsDataAndErrorsInJSON<ViewDataUser>(ModelState.GetErrors()));

        User user = new User()
        {
            Name = Users.UserName,
            Email = Users.UserEmail,
            PasswordHash = Users.UserPassword
        };

        PasswordHasher.Hash(user.PasswordHash);
        
        try
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewsDataAndErrorsInJSON<ViewDataUser>(Users));
        }
        catch (DbException e)
        {
            return BadRequest(StatusCode(200,new ResultViewsDataAndErrorsInJSON<string>("falha interna no servidor")));
        }
        
       
    }

    [HttpPost("v1/login")]
    public async Task<IActionResult> Login(
        [FromServices] GenerateTokenService token,
        [FromServices] Context context)
    {
        var EncryptationToken = token.GenerateToken(null);

        return Ok(EncryptationToken);
    }
}

