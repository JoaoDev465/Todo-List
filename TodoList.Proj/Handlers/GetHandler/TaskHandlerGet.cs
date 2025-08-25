using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoListCore;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IGetHandler;

namespace TodoList.Proj.Handlers.GetHandler;

[ApiController]
public class TaskHandlerGet(Context context):ITaskHandlerGet
{
    [Authorize("user")]
    [HttpGet]
    [Route("api/v1/tasks")]
    public async Task<PageResponse<List<Todo>>> GetTaskListAsync(TodoDto request)
    {
       

        var totalItens = await context.Todos.CountAsync();

        var pagenumber = Configurations.defaultpagenumber;

        var pagesize = Configurations.defaultpagesize;

        var tasks = await context.Todos.Skip
            ((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
        
        return new PageResponse<List<Todo>>(tasks,totalItens,totalItens);
    }
    [Authorize("user")]
    [HttpGet("api/v1/task")]
    public async  Task<Responses<Todo?>> GetByIdAsync( [FromQuery] TodoDto request)
    {
      
        var task = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (task == null)
        {
            Console.WriteLine($"id recebido , {request.Id}");
            return new Responses<Todo?>(null,404,"usuário não encontrado");
        }

        return new Responses<Todo?>(task, 200, "usuário");
    }
}