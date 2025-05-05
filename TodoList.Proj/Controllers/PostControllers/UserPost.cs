using System.Data.Common;
using ApiKeyatributte.Usage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;
using View.ViewModels;
using ViewModels.ResultViews;
using ViewModels.User;

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
        [FromBody] ViewDataUser users)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewsDataAndErrorsInJSON<ViewDataUser>
                    (ModelState.GetErrors()));

        var user = new User
        {
            Name = users.UserName,
            Email = users.UserEmail
        };

        users.UserPassword = PasswordGenerator.
            Generate(16);
        user.PasswordHash = PasswordHasher.
            Hash(users.UserPassword);
        
        try
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewsDataAndErrorsInJSON<dynamic>(new
            {
                user = users.UserEmail,users.UserPassword,user.PasswordHash
            }));
        }
        catch (DbException e)
        {
            return BadRequest(StatusCode
                (200,new ResultViewsDataAndErrorsInJSON<string>
                    ("falha interna no servidor")));
        }
        
       
    }
    
    
    [HttpPost("V1/Login")]
    public async Task<IActionResult> Login(
        [FromServices] GenerateTokenService token,
        [FromBody] ViewLogin view,
        [FromServices] Context context)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewsDataAndErrorsInJSON<string>
                    (ModelState.GetErrors()));

        var user = context.Users
            .AsNoTracking().FirstOrDefault
                (x => x.Email == view.UserEmail);
        if (user == null)
            return StatusCode(403,new 
                ResultViewsDataAndErrorsInJSON<String>
                ( "não foi possível encontrar o usuário"));

        if (!PasswordHasher.Verify(user.PasswordHash, view.UserPassword))
        {
            return StatusCode(400, 
                new ResultViewsDataAndErrorsInJSON<string>
                    ("senha inválida, tente novamente"));
        }

        try
        {
            var securityToken = token.TokenGenerator(user);
            return Ok(new ResultViewsDataAndErrorsInJSON<string>(securityToken,null));
        }
        catch (Exception e)
        {
            return BadRequest(new
                ResultViewsDataAndErrorsInJSON<string>
                ("falha ao tentar gerar o token"));
        }
    }
}

