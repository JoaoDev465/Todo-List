using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore.IHandlers.IPutHandler;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoList.Proj.Handlers.PutHandlers;

public class PutTaskHandler(Context context):IPutTaskHandler
{
        [HttpPut]
        [Route("api/v1/task/{id}")]
       public  async Task<Responses<Todo?>> PutAsync(TodoDTO request)
        {
            var content = new Todo
            {
                Task = request.Task,
                Description = request.DescriptionOfTask
            };
            var task = context.Todos.FirstOrDefaultAsync
                (x => x.Id== request.TaskId && request.TaskId == x.Id);
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

            return new Responses<Todo?>(content, 200, $"tarefa {request.TaskId} atualizada");

        }
    }