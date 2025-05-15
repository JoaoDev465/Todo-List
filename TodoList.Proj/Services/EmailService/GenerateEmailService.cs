using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using Resend;

namespace TodoList.Proj.Services.EmailService;



public class GenerateEmailService 
{
    private readonly SmtpClient? _smtpClient;

    public GenerateEmailService()
    {
        _smtpClient = new SmtpClient(Configuration._SmTpService.Host,
            Configuration._SmTpService.Port);

        _smtpClient.Credentials = new NetworkCredential(
            Configuration._SmTpService.Username,
            Configuration._SmTpService.Password);

        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        _smtpClient.EnableSsl = true;
    }
    
    public bool SendEmailusingConfigurationsSMTP(string Subject,
        string Body,
        string ToEmail, 
        string FromEmail = "joaodev465@gmail.com")
    {

        MailMessage mail = new MailMessage()
        {
            From = new MailAddress(FromEmail),
            Subject = Subject,
            Body = Body
        }; mail.To.Add(new MailAddress(ToEmail));
        
        try
        {
            _smtpClient.Send(mail);
            Console.WriteLine("Email enviado com sucesso");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}
