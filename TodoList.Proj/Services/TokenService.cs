using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj.Models;
using TodoList.Proj.Models.user;

namespace TodoList.Proj.TokenGenerator;
// this class configure token to user
public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        // the key need be transleet to bytes 
        var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key)
                ,SecurityAlgorithms.HmacSha256Signature)
            
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}