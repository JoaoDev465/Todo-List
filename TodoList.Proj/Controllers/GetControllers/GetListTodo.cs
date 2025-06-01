using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.GetControllers;

public class GetListTodoController : ControllerBase
{
    [Authorize]
    [HttpGet("v1/list/task")]
    public async Task<IActionResult> Get_List_User(
        [FromServices] Context context)
    {
        var tasks = await context.Todos.Select(x => new Todo()
        {
           Id = x.Id,
           Start = x.Start,
           Initialized = x.Initialized,
           Task = x.Task,
           Description = x.Description,
           Alert = x.Alert,
           Finalized = x.Finalized,
           User = x.User,
           UserId = x.UserId
           
            
        }).ToListAsync();

        return Ok(new ResultViewsDataAndErrorsInJSON
            <List<Todo>>(tasks));
    }
}