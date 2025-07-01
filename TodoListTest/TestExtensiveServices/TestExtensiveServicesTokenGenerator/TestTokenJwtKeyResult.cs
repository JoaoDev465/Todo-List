using System.Text;
using Microsoft.IdentityModel.Tokens;
using Moq;
using RestSharp.Authenticators;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.Models;
using Xunit;

namespace TodoListTest.TestExtensiveServices.TestExtensiveServicesTokenGenerator;

public class TestTokenJwtKey
{
    private string Users { get; set; }
    private readonly Mock<IGenerateTokenService> _mockGenerateTokenServices;

    public TestTokenJwtKey()
    {
        _mockGenerateTokenServices = new Mock<IGenerateTokenService>();
    }


    [Fact]
    public void TestTokenJwtKeyResult_SholdStartWhit_eiHOIE_when_User123()
    {
      
    }
}