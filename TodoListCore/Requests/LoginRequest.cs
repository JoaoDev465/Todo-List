using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Requests;

public class LoginRequest : Request
{
    [Required(ErrorMessage = "o campo Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "o campo senha é obrigatório")]
    public string Passoword { get; set; } = string.Empty;
}