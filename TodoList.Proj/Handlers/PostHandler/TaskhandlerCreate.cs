using System.Security.Claims;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Proj.Data;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers;

namespace TodoList.Proj.Handlers.PostHandler;

[ApiController]
public class TaskhandlerCreate(Context context, IHttpContextAccessor accessor): ITaskHandlerCreate
{
    
 
    [Authorize("user")]
    [HttpPost]
    [Route("api/v1/post")]
    public async Task<Responses<Todo?>> CreateAsync(TodoDto request)
    {
        var user = accessor.HttpContext.User;
        var userIdclaim = user?.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdclaim == null)
        {
            return Responses<Todo?>.Error(null,404,"não encontrado");
        }
        
        var tasks = new Todo
        {
            Task = request.Task,
            Description = request.DescriptionOfTask,
            UserId = int.Parse(userIdclaim.Value)
        };

        try
        {
            await context.AddAsync(tasks);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Responses<Todo?>.Error(null, 500, "falha interna no servidor");
        }

        return new Responses<Todo?>(tasks, 201, "task criada com sucesso");
        
    }
}