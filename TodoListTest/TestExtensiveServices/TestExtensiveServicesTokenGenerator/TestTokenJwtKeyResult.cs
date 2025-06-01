using System.Text;
using Microsoft.IdentityModel.Tokens;
using Moq;
using RestSharp.Authenticators;
using TodoList.Proj.InterfaceModel;
using Xunit;

namespace TodoListTest.TestExtensiveServices.TestExtensiveServicesTokenGenerator;

public class TestTokenJwtKey
{
    
    private readonly Mock<IGenerateTokenService> _mockGenerateTokenServices;

    public TestTokenJwtKey()
    {
        _mockGenerateTokenServices = new Mock<IGenerateTokenService>();
    }


    [Fact]
    public void TestTokenJwtKeyResult_SholdStartWhit_eiHOIE_when_User123()
    {
        var testTokenResult = _mockGenerateTokenServices.Object;
        var userNameFromGenerateToken = new TokenModel{ user = "user123" };
        const string expectedResultToken = "eiHOIE@jiwojc32pokmowumxoimeomow24355okjfenom";

        _mockGenerateTokenServices.Setup
            (x => x.returnToken(userNameFromGenerateToken)).
            Returns(expectedResultToken).Verifiable();

        testTokenResult.returnToken(userNameFromGenerateToken);
        
        Xunit.Assert.NotNull(expectedResultToken);
        Xunit.Assert.StartsWith("eiHOIE",expectedResultToken);

    }
}