using Apicontext.File;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using TodoListCore.ControllersHandlers;
using TodoListCore.DTO;
using TodoListCore.IHandlers;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using View.ViewModels;

namespace TodoList.Proj.Controllers.PostControllers;

public class LoginHandler(Context context, IGenerateTokenService service): ILoginHandler
{
 

    public async Task<Responses<TokenResponse?>> LoginAsync(LoginDTO request)
    {
        var user = await context.Users.FirstOrDefaultAsync(x=>x.Email== request.UserEmail);

        if (user is null)
        {
             return Responses<TokenResponse?>.Error(null, 404, "usuário não encontrado");
        }

        var hasher = PasswordHasher.Verify(user.PasswordHash, request.UserPassword);
        if (hasher is false)
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