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
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/user")]

public class GetListUserController: IUserPost
{
    private readonly Context _context;
    public GetListUserController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<PageResponse<List<User>>> Getlistasync(
        UserDto request, GetAllDatasDTO data)
    {
        try
        {
            var query =  _context.Users.AsNoTracking()
                .Where(x=>x.Id == request.Id)
                .OrderBy(x=>x.Name);
            
            var user = await _context
                .Users
                .Skip(data.PageSize - 1 * (data.PageNumber))
                .Take(data.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PageResponse<List<User>>(user,
                count, data.PageSize,
                data.PageNumber);
        }
        catch (Exception e)
        {
            return new PageResponse<List<User>>(null, 500, "Falha Interna No Servidor");
        } 
    }
}