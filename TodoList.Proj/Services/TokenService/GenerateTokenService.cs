using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoListCore.Interfaces;
using TodoListCore.Models;

namespace TodoList.Proj.Services.TokenService;

public class GenerateTokenService: IGenerateTokenService
{

    private readonly string? _JwtKey;
    private readonly JwtSecurityTokenHandler _securityTokenHandler;

    public GenerateTokenService(IConfiguration configuration)
    {
        _JwtKey = configuration["JwtSettings:secret"];
        _securityTokenHandler = new JwtSecurityTokenHandler();
    }
    

    public  string TokenGenerator(User user)
    {
        var claimfromuser = new[]
        {
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
        };
        var securitySymmetricKey = _SymmetricSecurityKey();
        var tokenDescriptorConf = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimfromuser),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(securitySymmetricKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = _securityTokenHandler.CreateToken(tokenDescriptorConf);
        Console.WriteLine(token);
        return _securityTokenHandler.WriteToken(token);
    }

    public  SymmetricSecurityKey _SymmetricSecurityKey()
    {
        var key = Encoding.UTF8.GetBytes(_JwtKey);
        Console.WriteLine(_JwtKey);
        return new SymmetricSecurityKey(key);
    }

    
}