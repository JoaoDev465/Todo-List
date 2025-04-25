using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Models;
using TodoList.Proj.Models.user;

namespace TodoList.Proj.TokenGenerator;
// this class configure token to user
public class TokenService
{
    public string GenerateToken(User user)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        // the key need be transleet to bytes 
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
}