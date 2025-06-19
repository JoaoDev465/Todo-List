using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.DTOview;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.GetControllers;

public class GetListTodoController : ControllerBase
{
    private readonly Context _context;

    public GetListTodoController(Context context)
    {
        _context = context;
    }
    
   
    [HttpGet("v1/list/task")]
    public async Task<IActionResult> Get_List_User(
        QueryPagination query)
    {
        var totaltasks = await _context.Todos.CountAsync();
        
        var tasks = await _context.Todos.
            AsNoTracking().Select(x=> new TodoDTO
            {
                DescriptionOfTask = x.Description,
                Task = x.Task,
                InitializeDateTimeTask = x.Initialized,
                FinalizedTimeTask = x.Finalized
                
            }).Skip((query.Page - 1) * query.PageSize).
            Take(query.PageSize).ToListAsync();

        var result = new
        {
            Total = totaltasks,
            ActualPage = query.Page,
            PageSizes = query.PageSize,
            Tasks = tasks
        };

        return Ok(new ResultViewsDataAndErrorsInJSON
            <dynamic>(new
            {
                result
            }));
    }
}