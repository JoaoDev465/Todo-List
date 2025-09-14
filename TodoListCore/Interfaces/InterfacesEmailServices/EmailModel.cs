namespace TodoListCore.Interfaces.InterfacesEmailServices;

public interface IGenerateEmailService
{
    public void IsendEmail(string Body,
        string Subject,
        string FromEmail,
        string ToEmail);
}