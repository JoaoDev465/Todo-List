using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.Todo;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/todo/{id:int}")]

public class GetTodoController : IUserPost
{
    private readonly Context _context;

    public GetTodoController(Context context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<Responses<Todo>> Getasync(
        TodoDTO request
       )
    {
        try
        {
             
            var tasks = await _context
                .Todos
                .Where(x => x.Id == request.TaskId)
                .FirstOrDefaultAsync();

            return tasks is null ? new Responses<Todo>(tasks, 404, "Task Não Encontrada")
                :  new Responses<Todo>(tasks);
        }
        catch (Exception e)
        {
            return new Responses<Todo>(null, 500, "Falha Interna No Servidor");
        }
    }
}