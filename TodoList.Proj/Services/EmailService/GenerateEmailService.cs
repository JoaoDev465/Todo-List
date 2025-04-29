using System.Net;
using System.Net.Mail;

namespace TodoList.Proj.Services.EmailService;



public class GenerateEmailService
{
    private readonly MailMessage _mailMessageService = new MailMessage();
   private readonly SmtpClient _smtpClientConfigurations = new SmtpClient();
   public string ToName { get; set; }
   public string ToEmail { get; set; }
   public string Fromname { get; set; }
   public string FromEmail { get; set; }
   public string Subject { get; set; }
   public string Body { get; set; }

   public SmtpClient _SmtpClient()
   {
      _smtpClientConfigurations.Port = Configuration._SmTpService.Port;
      _smtpClientConfigurations.Host = Configuration._SmTpService.Host;
      _smtpClientConfigurations.EnableSsl = true;
      _smtpClientConfigurations.DeliveryMethod = SmtpDeliveryMethod.Network;
      _smtpClientConfigurations.Credentials = new NetworkCredential(Configuration._SmTpService.Username,Configuration._SmTpService.Password);

      return _SmtpClient();
   }

   public void _MailMessage()
   {
       _mailMessageService.From = new MailAddress(Fromname, FromEmail);
       _mailMessageService.To.Add(new MailAddress(ToEmail,ToName));
       _mailMessageService.Subject = Subject;
       _mailMessageService.Body = Body;
   }

   public bool Send()
   {
       try
       {
           _MailMessage();
       
           var smtpClient = new SmtpClient();
           smtpClient.Send(_mailMessageService);

           return true;
       }
       catch (Exception e)
       {
           Console.WriteLine(e);
           return false;
       }
   }
}
