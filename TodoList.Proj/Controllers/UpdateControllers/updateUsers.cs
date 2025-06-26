using System.Data;
using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.UpdateControllers;


[ApiController]
[Route("api/v1/user/{id:int}")]

public class UpdateUserController : ControllerBase
{
   private readonly Context _context;
   public UpdateUserController(Context context)
   {
      _context = context;
   }
   
  
   [HttpPut]
   public async Task<IActionResult> UpdateUser(
      [FromRoute] int id,
      [FromBody] UserDto newuser)
   {
      if (!ModelState.IsValid)
         return BadRequest(new ResultViewsDataAndErrorsInJSON
            <string>(ModelState.GetErrors()));
      
      var user = await _context.
         Users.FirstOrDefaultAsync(x => x.Id == id);

      if (user == null)
      {
         return NotFound(new ResultViewsDataAndErrorsInJSON
            <User>(user));
      }

      user.Name = newuser.UserName;
      user.Email = newuser.UserEmail;
      user.PasswordHash = newuser.UserPassword;

      try
      {
         _context.Update(user);
         await _context.SaveChangesAsync();
      }
      catch (Exception e)
      {
         return BadRequest(new ResultViewsDataAndErrorsInJSON
            <User>(user));
      }

      return Ok(new ResultViewsDataAndErrorsInJSON
         <User>(user));
   }
}