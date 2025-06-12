using System.Data.Common;
using System.Net.Mail;
using ApiKeyatributte.Usage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureIdentity.Password;
using TodoList.Proj.Extensions.ExtensiveObjects;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;
using View.ViewModels;
using ViewModels.ResultViews;
using ViewModels.User;

namespace TodoList.Proj.Controllers.PostControllers;

using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using ViewModels.ResultViews;
using ViewModels.User;
using static Models.Role;

[AttributeKey]
[ApiController]
[Route("V1")]
public class PostController : ControllerBase
{
   
    
    [HttpPost("post/user")]
    public async Task<IActionResult> Post_User(
        [FromServices] Context context,
        [FromBody] ViewDataUser users)
    {
        if (!ModelState.IsValid)
            return BadRequest(
                new ResultViewsDataAndErrorsInJSON<ViewDataUser>
                    (ModelState.GetErrors()));
        return Ok();
    }
    
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(
        [FromServices] GenerateTokenService token,
        [FromBody] ViewLogin view,
        [FromServices] Context context)
    {
        var user = new User();
      
            var securityToken = token.TokenGenerator(user);
            return Ok(new ResultViewsDataAndErrorsInJSON<string>(securityToken,null));
      
    }
}

