using System.Net.Mail;
using TodoListCore.Interfaces.InterfacesEmailServices;

namespace TodoList.Proj.Services.EmailService;

public class SmtPClientWrapper: ISmtpClientWrapper
{
    private readonly ISmtpClientWrapper _smtpConfiguration;

    public SmtPClientWrapper(ISmtpClientWrapper configuration)
    {
        _smtpConfiguration = configuration;
    }

    public void Send(MailMessage message)
    {
        _smtpConfiguration.send(message);
    }
}