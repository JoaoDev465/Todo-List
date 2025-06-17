using System.Data.Common;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureIdentity.Password;
using TodoList.Proj.Atributtes.ApiKeyAtributte;
using TodoList.Proj.Extensions.ExtensiveObjects;
using TodoList.Proj.InterfaceModel;
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
using ViewModels.ResultViews;
using ViewModels.User;
using static Models.Role;

[AttributeKey]
[ApiController]
[Route("V1")]
public class PostController(IGenerateTokenService tokenService) : ControllerBase
{
    [HttpPost("post/user")]
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
            Email = users.UserEmail,
            Slug = users.UserEmail.Replace("@","-").Replace(".","-")
           
        };
        
        user.PasswordHash = PasswordHasher.Hash(users.UserPassword);
        try
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(new ResultViewsDataAndErrorsInJSON<dynamic>(new
            {
                users.UserName,
                users.UserEmail,
                users.UserPassword,
                user.PasswordHash,
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


    [HttpPost("userlogin")]
    public async Task< ActionResult> Login(
        [FromServices] Context context,
        [FromBody] ViewLogin viewLogin)
    {
        var user = new User();

        var userInDatabase = await context.Users.Include(x=>x.Roles).
            FirstOrDefaultAsync(x => x.Email == viewLogin.UserEmail);
       
        if (userInDatabase == null)
        {
            return StatusCode(404, new ResultViewsDataAndErrorsInJSON<string>
                ("usuário não encontrado no servidor"));
        }

        if (!PasswordHasher.Verify(userInDatabase.PasswordHash, viewLogin.UserPassword))
        {
            return BadRequest(new ResultViewsDataAndErrorsInJSON<string>("a senha está incorreta"));
        }

        try
        {
            
            string security = tokenService.TokenGenerator(userInDatabase);
            return Ok(security);
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewsDataAndErrorsInJSON<string>("falha ao gerar o token"));
        }
    }
}

