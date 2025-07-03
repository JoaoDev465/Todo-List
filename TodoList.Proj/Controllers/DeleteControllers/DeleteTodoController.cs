using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.DeleteControllers;

[ApiController]
[Route("api/v1/todos/{id:int}")]

public class DeleteTodoController : ITaskPost
{
    private readonly Context _context;

    public DeleteTodoController(Context context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<Responses<Todo>> Deleteasync(
        TodoDTO request
        )
    {
        var task = await _context.Todos.FirstOrDefaultAsync(x => x.Id == request.TaskId);

        if (task.Id == null)
        {
            return new Responses<Todo>(null, 404, "Tarefa Não Encontrata");
        }

        try
        {
            _context.Remove(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Responses<Todo>(null, 500, "Falha Interna No Servidor");
        }

        return new Responses<Todo>(task, 200, "Tarefa Excluida Com Sucesso");

    }
}