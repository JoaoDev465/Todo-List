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
using TodoListCore.Interfaces;
using TodoListCore.Response;
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
[Route("api/v1/login")]

public class LoginController : ILoginPost
{
    private readonly GenerateTokenService _tokenService;
    private readonly Context _context;
   
    public LoginController(GenerateTokenService tokenService,
        Context context)
    {
        _tokenService = tokenService;
        _context = context;
       
    }
    [HttpPost]
    public async Task<Responses<string>> Loginasync(
         LoginDTO request)
    {

        var userInDatabase = await _context.Users.Include(x=>x.Roles).
            FirstOrDefaultAsync(x => x.Email == request.UserEmail);
       
        if (userInDatabase == null)
        {
            return new Responses<string>(null, 404,
                ("usuário não encontrado no servidor"));
        }

        if (!PasswordHasher.Verify(userInDatabase.PasswordHash, request.UserPassword))
        {
            return new Responses<string>(null,404,"a senha está incorreta");
        }

        try
        {
            
            string security =  _tokenService.TokenGenerator();
            return new Responses<string>(security,200,"Ok");
        }
        catch (Exception e)
        {
            return new Responses<string>(null,500,("falha ao gerar o token"));
        }
    }
}

