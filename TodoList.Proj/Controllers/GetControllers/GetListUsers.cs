using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.DTOview;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.GetControllers;

[ApiController]
[Route("api/v1/user")]

public class GetListUserController: ControllerBase
{
    private readonly Context _context;
    public GetListUserController(Context context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get_List_User(
        QueryPagination query )
    {
        var totalUsers = await _context.Users.CountAsync();
        var users = await _context.Users.AsNoTracking().Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
            .Select(x => new UserDto
            {
                Id = x.Id,
                UserEmail = x.Email,
                UserPassword = x.Email,
                UserAreOnline = x.IsOnline,
                UserName = x.Name
            }).ToListAsync();

        var result = new
        {
            TotalUser = totalUsers,
            Pages = query.Page,
            PageSizes = query.PageSize,
            Users = users

        };
        
        return Ok(new ResultViewsDataAndErrorsInJSON
            <dynamic>(result));
    }
}