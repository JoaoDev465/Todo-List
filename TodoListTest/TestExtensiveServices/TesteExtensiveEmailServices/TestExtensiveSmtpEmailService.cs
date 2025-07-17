using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Moq;
using TodoList.Proj.InterfaceModel;
using TodoList.Proj.InterfaceModel.InterfacesEmailServices;
using Xunit;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace TodoListTest.TestExtensiveServices;

public class TestExtensiveSmtpEmailService
{
    private readonly IConfigurationRoot _configuration;
    private readonly Mock<ISmtpConfiguration> _mockSmtpConf;

    public TestExtensiveSmtpEmailService()
    {
        _configuration = new ConfigurationBuilder().
            AddJsonFile("appsettings.Development.tests.json").
            AddEnvironmentVariables().Build();
        _mockSmtpConf = new Mock<ISmtpConfiguration>();
    }

    [Fact]
    public void TestSmtpConfigure()
    {
        _mockSmtpConf.Setup(x => x.Configurations(It.Is<SmtpModel>(
            x => x.Port.ToString() == _configuration["ConfSMTP:port"] 
                 && x.Host == _configuration["ConfSMTP:host"]
                 && x.UserName == _configuration["ConfSMTP:username"]
                 && x.Password == _configuration["ConfSMTP:password"]))).Verifiable();

        var TestConfigurationSmtp = _mockSmtpConf.Object;
        
        TestConfigurationSmtp.Configurations(new SmtpModel()
        {
            Port  = _configuration["ConfSMTP:port"],
            Host = _configuration["ConfSMTP:host"],
            UserName = _configuration["ConfSMTP:username"],
            Password = _configuration["ConfSMTP:password"]
        });
        
        _mockSmtpConf.Verify(x=>x.Configurations(
            It.IsAny<SmtpModel>()),Times.Once());
    }
}