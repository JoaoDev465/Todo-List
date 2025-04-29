using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models.user;

namespace TodoList.Proj.Services.TokenService;

public class GenerateTokenService
{
    private readonly JwtSecurityTokenHandler _securityTokenHandler = new JwtSecurityTokenHandler();
    
    public string GenerateToken(User user)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var claim = user.GetClaim();
        var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key)
                ,SecurityAlgorithms.HmacSha256Signature)
            
        };
        var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        return jwtSecurityTokenHandler.WriteToken(token);
    }

    private SymmetricSecurityKey _symmetricSecurityKey()
    {
        var securityKey = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        return new SymmetricSecurityKey(securityKey);
    }
    
}