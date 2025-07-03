using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/user/{id:int}")]

public class GetTaskController:IUserPost
{
    private readonly Context _context;

    public GetTaskController(Context context)
    {
        _context = context;
    }
   
    [HttpGet]
    public async Task<Responses<User>> Getasync(
       UserDto request)
    {
        try
        {
            var users = await _context
                .Users
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            return users is null
                ? new Responses<User>(null, 404, "Usuário Não Encontrado")
                : new Responses<User>(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}