using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IDeleteHandlers;

namespace TodoList.Proj.Handlers.DeleteHandler;

[ApiController]
public class DeleteTaskHandler(Context context) : IDeleteTasksHandler
{
    
    [Authorize("user")]
    [HttpPost]
    [Route("api/v1/tasks/delete")]
    public async Task<Responses<Todo?>> DeleteAsync([FromBody]TodoDto request)
    {
       
        var task = await  context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id);
        
        if (task is null)
        {
            return Responses<Todo?>.Error(null,404,"tarefa não encontrada");
        }

        try
        {
           context.Todos.Remove(task);
           await  context.SaveChangesAsync();
        }
        catch (Exception e)
        {
          return Responses<Todo?>.Error(null,500,"falha interna no servidor");
        }

        return new Responses<Todo?>(task, 200, "tarefa excluída com sucesso");
    }
}