using System.Security.Claims;
using TodoList.Proj.Models;

namespace TodoList.Proj.Extensions.ExtensiveObjects;

public static class ExtensiveClaims
{
   public static IEnumerable<Claim> GetClaim(this User
       user)
    {
       
        var result = new List<Claim>
        {
            new (ClaimTypes.Name,ClaimTypes.Email)
        };

        if (user?.Roles != null && user.Roles.Any())
        {
            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role,role.Name)));

        }
        
        return result;
    }
}