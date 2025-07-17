using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Testing.Platform.Configurations;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using TodoList.Proj;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.InterfaceModel.InterfacesEmailServices;
using TodoList.Proj.Services.EmailService;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TodoListTest.TestExtensiveServices;

public class TestExtensiveEmailSendService
{
   private readonly Mock<IGenerateEmailService> _mockServiceEmail;

   public TestExtensiveEmailSendService()
   {
      _mockServiceEmail = new Mock<IGenerateEmailService>();
   }
   
   [Fact]
   public void TestEmailSendServices()
   {
      _mockServiceEmail.Setup(
         x => x.IsendEmail(
            It.Is<EmailModel>(e=>e.Subject == "ola" 
                                 && e.Body == "cumprimentar"
                                 && e.FromEmail == "joaodev465@gmail.com"
                                 && e.ToEMail == "joaodev465@gmail.com"))).Verifiable();

      var EmailGenerator = _mockServiceEmail.Object;
      
      EmailGenerator.IsendEmail(new EmailModel()
      {
         Subject = "ola",
         Body = "cumprimentar",
         FromEmail = "joaodev465@gmail.com",
         ToEMail = "joaodev465@gmail.com"
      });
      
      _mockServiceEmail.Verify(x=>x.IsendEmail(
         It.IsAny<EmailModel>()),Times.Once());
      Assert.IsNotNull(EmailGenerator);
   }
}