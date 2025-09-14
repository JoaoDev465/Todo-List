using System.Net.Mail;
using TodoListCore.Interfaces.InterfacesEmailServices;

namespace TodoList.Proj.Services.EmailService;

public class SmtPClientWrapper: ISmtpClientWrapper
{
    private readonly string? _Configuration;

    public SmtPClientWrapper(IConfiguration conf)
    {
      
       _Configuration = conf["ConfSMTP"];
    }

    public void Send(MailMessage message)
    {
        
    }
}