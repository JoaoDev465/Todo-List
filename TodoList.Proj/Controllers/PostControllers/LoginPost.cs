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
public class LoginController : ControllerBase
{
    private readonly IGenerateTokenService _tokenService;
    private readonly Context _context;
   
    public LoginController(IGenerateTokenService tokenService,
        Context context)
    {
        _tokenService = tokenService;
        _context = context;
       
    }

    [HttpPost("userlogin")]
    public async Task< ActionResult> Login(
        [FromBody] ViewLogin viewLogin)
    {

        var userInDatabase = await _context.Users.Include(x=>x.Roles).
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
            
            string security = _tokenService.TokenGenerator(userInDatabase);
            return Ok(security);
        }
        catch (Exception e)
        {
            return BadRequest(new ResultViewsDataAndErrorsInJSON<string>("falha ao gerar o token"));
        }
    }
}

