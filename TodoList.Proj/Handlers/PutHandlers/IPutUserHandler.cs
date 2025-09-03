using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("api/v1/user/{request.id}")]
    public async Task<Responses<User?>> PutAsync([FromRoute]UserDto request)
    {
        var password = hasher.HashPassword(null, request.UserPassword);
        var user = await context.Users.FirstOrDefaultAsync
            (x => x.Id == request.Id);

        user.Email = request.UserEmail;
        user.PasswordHash = password;
       
        if (user is null)
        {
            return new Responses<User?>(null,404,"usuário não encontrado");
        }

        try
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
           return new Responses<User?>(null,500,"falha interna no servidor");
        }

        return new Responses<User?>(user, 200, $"user {request.Id} atualizado");

    }
}