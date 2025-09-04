using System.Net.Mail;
using Moq;
using TodoListCore.Interfaces.InterfacesEmailServices;
using Xunit;

namespace TodoListTest.MockTests;

public class TestSmtpService
{
     [Fact]
     public void TestSmtpWrapper_WhenGenericMail_IsValid()
     {
          var mail = new MailMessage();
          mail.Body = "ola";
          var MockISmtpWrapper = new Mock<ISmtpClientWrapper>();

          MockISmtpWrapper.Setup(x => x.send(mail)).Verifiable();

          MockISmtpWrapper.Object.send(mail);

          MockISmtpWrapper.Verify(x => x.send(mail),Times.Once);

     }
     
}