using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data;
using TodoList.Proj.Models;
using TodoListCore.IHandlers.IDeleteHandlers;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoList.Proj.Handlers.DeleteHandler;

public class DeleteTaskHandler(Context context) : IDeleteTasksHandler
{
    [HttpDelete]
    [Route("api/v1/tasks/{id}")]
    public async Task<Responses<Todo?>> DeleteAsync(TodoDTO request)
    {
        var content = new Todo
        {
            Task = request.Task,
            Description = request.DescriptionOfTask
        };
        var task = await  context.Todos.FirstOrDefaultAsync(x => x.Id == request.userId && request.userId == x.Id);
        if (task is null)
        {
            return Responses<Todo?>.Error(null,404,"usuário não encontrado");
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