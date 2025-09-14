using System.Security.Claims;
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
public class TaskHandlerGet(Context context, IHttpContextAccessor accessor) : ITaskHandlerGet
{
    [Authorize("user")]
    [HttpGet]
    [Route("api/v1/tasks")]
    public async Task<PageResponse<List<Todo?>>> GetTaskListAsync([FromQuery] GetAllDatasDto request)
    {
        var user = accessor?.HttpContext?.User;
        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return new PageResponse<List<Todo?>>(null, 401); 
        }

        var userId = int.Parse(userIdClaim.Value);

        try
        {
            var query = context
                .Todos
                .AsNoTracking()
                .Where(x => x.UserId == userId) 
                .OrderBy(x => x.Task);

            var tasks = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PageResponse<List<Todo?>>(
                tasks,
                count,
                200,
                tasks.Count,
                request.PageNumber
            );
        }
        catch (Exception)
        {
            return new PageResponse<List<Todo?>>(null, 500);
        }
    }

    [Authorize("user")]
    [HttpGet("api/v1/task/{id}")]
    public async Task<Responses<Todo?>> GetByIdAsync([FromRoute] int id)
    {
        var user = accessor?.HttpContext?.User;
        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return new Responses<Todo?>(null, 401, "usuário não autenticado");
        }

        var userId = int.Parse(userIdClaim.Value);

        try
        {
            var task = await context.Todos
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId); 

            if (task == null)
            {
                return new Responses<Todo?>(null, 404, "tarefa não encontrada ou não pertence a este usuário");
            }

            return new Responses<Todo?>(task, 200, $"tarefa {id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Responses<Todo?>(null, 500, "falha interna no servidor");
        }
    }
}
