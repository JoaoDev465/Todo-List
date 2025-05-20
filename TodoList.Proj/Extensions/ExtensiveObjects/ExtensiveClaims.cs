using System.Security.Claims;
using TodoList.Proj.Models.user;

namespace TodoList.Proj.ExtensionMethods;

public static class ExtensiveClaims
{
   public static IEnumerable<Claim> GetClaim(this User user)
    {
        var result = new List<Claim>
        {
            new (ClaimTypes.Role,ClaimTypes.Email)
        };
        result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role,role.Name)));

        return result;
    }
}