using Moq;
using TodoListCore.Interfaces.InterfacesEmailServices;
using Xunit;

namespace TodoListTest.MockTests;

public class TestUnitEmailServices
{
    [Fact]
    public void TestEmail_WhenGenericMessage_IsValid()
    {
        string body = "ola";
        string subject = "teste";
        string FromEmail = "joaodev465@gmail.com";
        string ToEmail = "joaodev465@gmail.com";

        var mockEmailService = new Mock<IGenerateEmailService>();
        
        mockEmailService.Setup(
            x=>x.IsendEmail(body, subject, FromEmail, ToEmail)).Verifiable();
        
        mockEmailService.Object.IsendEmail(body, subject, FromEmail, ToEmail);

        mockEmailService.Verify(x=>x.IsendEmail(body, subject, FromEmail, ToEmail),Times.Once);
    }
}