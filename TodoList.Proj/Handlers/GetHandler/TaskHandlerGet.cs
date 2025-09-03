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
    public async Task<PageResponse<List<Todo?>>> GetTaskListAsync([FromQuery]GetAllDatasDto request)
    {
        try
        {
            var query =
                    context
                .Todos
                .AsNoTracking()
                .OrderBy(x => x.Task);

            var tasks = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize).ToListAsync();

            var count = await query.CountAsync();

            return new PageResponse<List<Todo?>>( tasks.ToList(),
                count,
                200,
                tasks.Count,
                request.PageNumber);

        }
        catch (Exception e)
        {
            return new PageResponse<List<Todo?>>(null, 500);
        }
    }
    

    [Authorize("user")]
    [HttpGet("api/v1/task/{request.id}")]
    public async  Task<Responses<Todo?>> GetByIdAsync( [FromRoute] int id)
    {

        try
        {
            var task = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                Console.WriteLine($"id recebido , {id}");
                return new Responses<Todo?>(null,404,"tarefa não encontrada");
            }
            return new Responses<Todo?>(task, 200, $"tarefa {id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
    }
}