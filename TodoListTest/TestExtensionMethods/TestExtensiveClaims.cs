using System.Security.Claims;
using TodoList.Proj.Models.user;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TodoListTest.ExtensionsTests;


public class TestExtensiveClaims
{
    [Fact]
    public void TestExtensiveClaims_ReturnListClaims()
    {
        
        var ClientClaimType = new List<Claim>()
        {
            new Claim(ClaimTypes.Role, "user")
        };
        
        Assert.IsNotNull(ClientClaimType);
        Assert.AreEqual(1,ClientClaimType.Count);
    }
}