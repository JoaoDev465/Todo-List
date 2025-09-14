using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Uses_Cases.DTO;

public class LoginDto : Request
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&])(?=.*[\d])([A-Za-z!@#$%&\d]){8,}$",
        ErrorMessage = "a senha precisa conter pelo menos 1 letra maiúscula, 1 minúscula, 1 caractere especial ex: '@!$%&#' e 8 caracteres " )]
    public string UserPassword { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; } = String.Empty;

    public List<int>? Roles { get; set; }
}