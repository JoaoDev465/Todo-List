using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.DTOview;
using TodoList.Proj.Models;
using TodoListCore.DTO;
using TodoListCore.Interfaces;
using TodoListCore.Response;
using ViewModels.ResultViews;
using ViewModels.Todo;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/task")]

public class GetListTodoController : ITaskPost
{
    private readonly Context _context;

    public GetListTodoController(Context context)
    {
        _context = context;
    }
    
   
    [HttpGet]
    public async Task<PageResponse<List<Todo>>> Getlistasync(
        TodoDTO request, GetAllDatasDTO? data)
    {
        try
        {
            var query =
                    _context.Todos.AsNoTracking()
                    .Where(x => x.Id == request.userId)
                    .OrderBy(x => x.Task);

            var task = await query.Skip(data.PageSize - 1 * (data.PageNumber))
                .Take(data.PageNumber)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PageResponse<List<Todo>>(task, count, data.PageNumber, data.PageSize);
        }
        catch
        {
            return new PageResponse<List<Todo>>(null, 500, "Falha Interna No Servidor");
        }
    }
}