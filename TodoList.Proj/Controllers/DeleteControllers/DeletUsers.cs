using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.DeleteControllers;


[ApiController]
[Route("api/v1/user/{id:int}")]

public class DeleteUserController : IUserPost
{
    private readonly Context _context;

    public DeleteUserController(Context context)
    {
        _context = context;
    }
    
    [HttpDelete]
    public async Task<Responses<User>> Deleteasync(
      UserDto request )
    {
        var user = await _context.
            Users.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (user.Id == null)
        {
            return new Responses<User>(null,404,
                    "Usuário Não Encontrado");
        }

        try
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Responses<User>(null,500,"Falha Interna No Servidor");
        }

        return new Responses<User>(user,200,"Usuário Excluido Com Sucesso");
    }
}