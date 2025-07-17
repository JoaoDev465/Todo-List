using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using TodoList.Proj.Models;
using TodoListCore.ControllersHandlers;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoList.Proj.Handlers.PostHandler;

public class TaskhandlerCreate(Context context): ITaskHandlerCreate
{
    [HttpPost]
    [Route("api/v1/post")]
    public async Task<Responses<Todo?>> CreateAsync(TodoDTO request)
    {
        var tasks = new Todo
        {
            Task = request.Task,
            Description = request.DescriptionOfTask
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