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
    
    public string TokenGenerator(User user)
    {
        var roleClaim = user.GetClaim();
        var securitySymmetricKey = _SymmetricSecurityKey();
        var tokenDescriptorConf = new SecurityTokenDescriptor()
        {
            Subject = new  ClaimsIdentity(roleClaim),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(securitySymmetricKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _securityTokenHandler.CreateToken(tokenDescriptorConf);

        return _securityTokenHandler.WriteToken(token);
    }

    public SymmetricSecurityKey _SymmetricSecurityKey()
    {
        var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        return new SymmetricSecurityKey(key);
    }
    
    
    

}