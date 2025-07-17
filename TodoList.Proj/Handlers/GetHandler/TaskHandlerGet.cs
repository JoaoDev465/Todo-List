using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoListCore;
using TodoListCore.DTO;
using TodoListCore.IHandlers.IGetHandler;
using TodoListCore.Response;
using ViewModels.Todo;

namespace TodoList.Proj.Handlers.GetHandler;

public class TaskHandlerGet(Context context):ITaskHandlerGet
{
    [HttpGet]
    [Route("api/v1/tasks")]
    public async Task<PageResponse<Todo>> GetTaskListAsync(TodoDTO request)
    {
        var content = new Todo
        {
            Task = request.Task,
            Description = request.DescriptionOfTask
        };

        var totalItens = await context.Todos.CountAsync();

        var currentpage = Configurations.defaultpagenumber;

        var pageskip = context.Todos.Skip(currentpage - (Configurations.defaultpagesize)).Take(currentpage);

        return new PageResponse<Todo>(content,totalItens,currentpage);
    }

    [HttpGet("api/v1/task/{id}")]
    public async  Task<Responses<Todo?>> GetByIdAsync(TodoDTO request)
    {
        var content = new Todo
        {
            Task = request.Task,
            Description = request.DescriptionOfTask
        };
        var task = await context.Todos.FirstOrDefaultAsync(x => x.Id == request.TaskId);
        if (task is null)
        {
            return Responses<Todo?>.Error(null,404,"usuário não encontrado");
        }

        return new Responses<Todo?>(content, 200, "usuário");
    }
}