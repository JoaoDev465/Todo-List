using System.Data.Common;
using System.Net.Mail;
using System.Runtime.InteropServices.JavaScript;
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
using TodoListCore.ControllersHandlers;
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

[ApiController]
[Route("api/v1/register")]

public class LoginController(Context context, IPasswordHasher<User> hasher) : IRegisterHandler
{
    private readonly IGenerateTokenService _tokenService;
    private readonly Context _context;
    
    public int Code { get; set; }
    public string Message { get; set; }
    public string data { get; set; }
    
    [HttpPost]

    public async Task<Responses<User?>> RegisterAsync(UserDto request)
    {
        var user = new User
        {
            Email = request.UserEmail,PasswordHash = hasher.HashPassword(null,request.UserPassword)
        };
        if (await context.Users.AnyAsync(x => x.Email == request.UserEmail))
        {
            return Responses<User?>.Error(null, 400,"usuário já,existente");
        }
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return new Responses<User?>(user, 201, "usuário registrado com sucesso");
    }
}

