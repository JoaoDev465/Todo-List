using System.Diagnostics.SymbolStore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoListCore.Models;
using TodoListCore.Response;
using TodoListCore.Uses_Cases.DTO;
using TodoListCore.Uses_Cases.IHandlers.IPutHandler;

namespace TodoList.Proj.Handlers.PutHandlers;

[ApiController]
public class PutTaskHandler(Context context):IPutTaskHandler
{
    
        [Authorize("user")]
        [HttpPut]
        [Route("api/v1/task/{id:int}")]
       public  async Task<Responses<Todo?>> PutAsync([FromRoute] int id,[FromBody] TodoDto request)
       {
           var task = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
               
            if (task is null)
            {
                return new Responses<Todo?>(null,404,"tarefa não encontrada");
            }
            task.Task = request.Task;
            task.Description = request.DescriptionOfTask;

            try
            {
                context.Todos.Update(task);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new Responses<Todo?>(null,500,"falha interna no servidor");
            }

            return new Responses<Todo?>(task, 200, $"tarefa {request.Id} atualizada");

        }
    }