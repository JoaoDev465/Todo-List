using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoListCore.Interfaces;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;

namespace TodoList.Proj.Handlers.AuthHandlers;

[ApiController]
public class LoginHandler(Context context, IGenerateTokenService service, IPasswordHasher<User> hasher): ILoginHandler
{
   
    
    [HttpPost]
    [Route("api/v1/login")]
    public async Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x=>x.Email== request.UserEmail);

        if (user is null)
        {
             return Responses<TokenResponse?>.Error(null, 404, "usuário não encontrado");
        }

        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.UserPassword);
        if (result == PasswordVerificationResult.Failed)
        {
            return Responses<TokenResponse>.Error(null, 401, "senha incorreta");

        }

        var token = service.TokenGenerator(user);

        return new Responses<TokenResponse?>(new TokenResponse
        {
            Token = token
        });

    }
}