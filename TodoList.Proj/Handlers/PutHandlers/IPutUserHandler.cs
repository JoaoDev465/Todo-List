using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using TodoList.Proj.Atributtes.ApiKeyAtributte;
using TodoList.Proj.Data;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IPutHandler;

namespace TodoList.Proj.Handlers.PutHandlers;

[ApiController]
public class PutUserHandler(Context context, IPasswordHasher<User?> hasher):IPutUserHandler
{
    [Authorize("user")]
    [HttpPut]
    [Route("api/v1/user/{id}")]
    public async Task<Responses<User?>> PutAsync(UserDto request)
    {
        var content = new User
        {
            Email = request.UserEmail,
            PasswordHash = hasher.HashPassword(null,request.UserPassword)
        };
        var user = context.Users.FirstOrDefaultAsync
            (x => x.Id == request.Id && request.Id == x.Id);
        if (user is null)
        {
            return Responses<User?>.Error(null,404,"usuário não encontrado");
        }

        try
        {
            context.Users.Update(content);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
           return Responses<User?>.Error(null,500,"falha interna no servidor");
        }

        return new Responses<User?>(content, 200, $"user {request.Id} atualizado");

    }
}