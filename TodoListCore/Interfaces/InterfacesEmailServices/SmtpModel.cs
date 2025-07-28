using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Interfaces.InterfacesEmailServices;

public class SmtpModel
{
    [Required]
    public string? Host{ get; set; } = String.Empty;
    [Required]
    public string? Port { get; set; }
    [Required]
    public string? Password { get; set; } = String.Empty;
    [Required]
    public string? UserName { get; set; } = String.Empty;
}

public interface ISmtpConfiguration
{
    public void Configurations(SmtpModel smtpModel)
    {
        
    }
}