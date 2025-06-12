using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj.ExtensionMethods;
using TodoList.Proj.Extensions.ExtensiveObjects;
using TodoList.Proj.Models;
using TodoList.Proj.Services.EmailService;
using TodoList.Proj.Services.TokenService;

namespace TodoList.Proj.InterfaceModel;

public class TokenModel
{
    public string user;
}

public abstract class IGenerateTokenService
{
    private readonly JwtSecurityTokenHandler _securityTokenHandler;
    
    public virtual string TokenGenerator(User user)
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

    public virtual SymmetricSecurityKey _SymmetricSecurityKey()
    {
        var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        return new SymmetricSecurityKey(key);
    }
}