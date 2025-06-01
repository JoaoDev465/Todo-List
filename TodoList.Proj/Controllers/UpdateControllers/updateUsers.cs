using System.Data;
using Apicontext.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models.user;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.UpdateControllers;


[ApiController]
public class UpdateController : ControllerBase
{
   [Authorize]
   [HttpPut("v1/update/user/{id:int}")]
   public async Task<IActionResult> UpdateUser(
      [FromServices] Context context,
      [FromRoute] int id,
      [FromBody] ViewDataUser newuser)
   {
      if (!ModelState.IsValid)
         return BadRequest(new ResultViewsDataAndErrorsInJSON
            <string>(ModelState.GetErrors()));
      
      var user = await context.
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
         context.Update(user);
         await context.SaveChangesAsync();
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