using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Atributtes.ApiKeyAtributte;
using TodoList.Proj.Data;
using TodoListCore.Interfaces;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;

namespace TodoList.Proj.Handlers.AuthHandlers;

[ApiController]
public class RegisterHandler(Context context, IPasswordHasher<User?> hasher) : IRegisterHandler
{
   
    
   
    [HttpPost]
    [Route("api/v1/register")]
    public async Task<Responses<User?>> RegisterAsync(UserDto request)
    {
       
                                                           
                       
        var user = new User
        {
            Email = request.UserEmail,
           PasswordHash =   request.UserPassword,
           Roles = new List<Role>
           {
               new Role{Name = "user"}
           }
        };
        user.PasswordHash = hasher.HashPassword(null, request.UserPassword);
        
        if (await context.Users.AnyAsync(x => x.Email == request.UserEmail))
        {
            return Responses<User?>.Error(null, 400,"usuário já,existente");
        }
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return new Responses<User?>(user, 201, "usuário registrado com sucesso");
    }
}

