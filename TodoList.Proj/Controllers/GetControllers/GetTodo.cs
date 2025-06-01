using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.ResultViews;
using ViewModels.Todo;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

public class GetTodoController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/todo/{id:int}")]
    public async Task<IActionResult> Get_OneUser(
        [FromServices] Context context ,
        [FromRoute] int id)
    {
        var task = await context.Todos
            .Where(x => x.Id == id).Select(x => new ViewTodo()
            {
                userId = x.Id,
                Start_Task = x.Start,
                InitializeDateTimeTask = x.Initialized,
                Task = x.Task,
                DescriptionOfTask = x.Description,
                FinalizedTimeTask = x.Finalized
            }).FirstAsync();

        if (task == null)
            return NotFound(
                new ResultViewsDataAndErrorsInJSON<string>
                    ("usuário não encontrado"));

        return Ok(task);
    }
}