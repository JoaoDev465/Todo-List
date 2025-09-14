using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace TodoListCore.Interfaces.InterfacesEmailServices;

public interface ISmtpClientWrapper
{
     void send(MailMessage message)
    {
        
    }
}