using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.UpdateControllers;

[ApiController]
[Route("api/v1/todos/{id:int}")]

public class UpdateTodosController : ITaskPost
{
    private readonly Context _context;
    public UpdateTodosController(Context context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpPut]
    public async Task<Responses<Todo>> Updateasync(
         TodoDTO request)
    {
        
        var tasks = await _context.Todos
            .FirstOrDefaultAsync(x => x.Id == request.userId);

        if (tasks == null)
        {
            return new Responses<Todo>(null,404,"Tarefa Não Encontrata");
        }

        tasks.Start = request.Start_Task;
        tasks.Initialized = request.InitializeDateTimeTask;
        tasks.Task = request.Task;
        tasks.Description = request.DescriptionOfTask;
        tasks.Alert = request.AlertForDateTask;
        tasks.Finalized = request.FinalizedTimeTask;

        try
        {
            _context.Update(tasks);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return new Responses<Todo>(null,500,
                "Falha Interna No Servidor");
        }

        return new Responses<Todo>(tasks, 200, "Tarefa Atualizada Com Sucesso");
    }
}