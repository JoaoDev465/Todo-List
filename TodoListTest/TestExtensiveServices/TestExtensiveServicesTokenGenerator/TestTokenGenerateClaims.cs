using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoList.Proj;
using TodoList.Proj.Extensions.ExtensiveObjects;
using TodoList.Proj.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace TodoListTest.TestExtensiveServices.TestExtensiveServicesTokenGenerator;

public class TestTokenGenerateClaims
{
    private readonly JwtSecurityTokenHandler _jwtSecurityToken;
    private readonly SecurityTokenDescriptor _securityTokenDescriptor;
    
    public TestTokenGenerateClaims(JwtSecurityTokenHandler securityTokenHandler)
    {
        _jwtSecurityToken = securityTokenHandler;
      
    }
    [Fact]
    public void  TestClaimsInToken()
    {
        var claims = new []
        {
            new Claim("role","User")
        };
       
        var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);
        var tokensecurityDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
        };
      var token = _jwtSecurityToken.CreateToken(tokensecurityDescriptor);
       var tokenstringJWT = _jwtSecurityToken.WriteToken(token);

       var returnReadToken = _jwtSecurityToken.ReadJwtToken(tokenstringJWT);
       var tokenClaimRoleTypeUser = returnReadToken.Claims.FirstOrDefault
           (x => x.Type == "role")?.Value;

       Assert.NotNull(tokenClaimRoleTypeUser);
       Assert.Equal("User",tokenClaimRoleTypeUser);
      
      

    }
    
   
}