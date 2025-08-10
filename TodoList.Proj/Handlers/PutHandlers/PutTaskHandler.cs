using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Atributtes.ApiKeyAtributte;
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
        [Route("api/v1/task/{id}")]
       public  async Task<Responses<Todo?>> PutAsync([FromBody]TodoDto request)
        {
            var content = new Todo
            {
                Task = request.Task,
                Description = request.DescriptionOfTask
            };
            var task = await context.Todos.FirstOrDefaultAsync
                (x => x.Id== request.Id && request.Id == x.Id);
            if (task is null)
            {
                return Responses<Todo?>.Error(null,404,"tarefa não encontrada");
            }

            try
            {
                context.Todos.Update(content);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Responses<Todo?>.Error(null,500,"falha interna no servidor");
            }

            return new Responses<Todo?>(content, 200, $"tarefa {request.Id} atualizada");

        }
    }