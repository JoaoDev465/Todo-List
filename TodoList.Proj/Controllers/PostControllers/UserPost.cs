using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using SecureIdentity.Password;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using View.ViewModels;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.PostControllers;

[ApiController]
[Route("api/v1/user")]

public class UserController : IUserPost
{
    private readonly Context _context;

    public UserController(Context context)
    {
        _context = context;
    }
  
    [HttpPost]
    public async Task<Responses<User?>> Registerasync(UserDto request)
    {
        var user = new User
        {
            Name = request.UserName,
            Email = request.UserEmail,
            Slug = request.UserEmail.Replace("@","-").Replace(".","-")
           
        };
        
        user.PasswordHash = PasswordHasher.Hash(request.UserPassword);
        try
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return new Responses<User>(user, 201, "usuário criado com sucesso");
        }
        catch (Exception e)
        {
            return new Responses<User>(null, 500, "falha interna no servidor");
        }
      
    }

}