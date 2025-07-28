namespace TodoListCore.Interfaces.InterfacesEmailServices;

public class EmailModel
{
    public string Body { get; set; } =String.Empty;
    public string Subject { get; set; } = String.Empty;
    public string ToEMail { get; set; } = String.Empty;
    public string FromEmail { get; set; } = String.Empty;
}
public interface IGenerateEmailService
{
    public void IsendEmail(EmailModel model);
}