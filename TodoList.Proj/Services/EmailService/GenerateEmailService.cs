
using System.Net;
using System.Net.Mail;
using TodoListCore.Interfaces.InterfacesEmailServices;

namespace TodoList.Proj.Services.EmailService;




public class GenerateEmailService : IGenerateEmailService
{
    private readonly SmtPClientWrapper? _wrapper;
    

    public GenerateEmailService(SmtPClientWrapper? wrapper)
    {
       
        _wrapper = wrapper;
    }
    
    public GenerateEmailService()
    {
        var smtpClient = new SmtpClient
        (Configuration._SmTpService.Host, 
            Configuration._SmTpService.Port);

        smtpClient.Credentials = new NetworkCredential(
            Configuration._SmTpService.Username,
            Configuration._SmTpService.Password);

        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
    }

    public void IsendEmail(string body,
        string subject  ,
        string fromEmail ="joaodev465@gmail.com",
        string toEmail = "joaodev465@gmail.com")
    {
        MailMessage mail = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = body,
        }; mail.To.Add(toEmail);
        
        try
        {
            _wrapper.Send(mail);
            Console.WriteLine("Email enviado com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

   
}
