using Apicontext.File;
using Microsoft.AspNetCore.Mvc;
using TodoList.Proj.TokenGenerator;

namespace TodoList.Proj.Controllers;


[ApiController]
public class PostController : ControllerBase
{
    [HttpPost("v1/post/login")]
    public async Task<IActionResult> PostLogin(
        [FromServices] Context context,
        [FromServices] TokenService token
        )
    {
        var Token = token.GenerateToken(null);
        return Ok(Token);
    }
}