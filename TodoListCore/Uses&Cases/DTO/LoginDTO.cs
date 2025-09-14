using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Uses_Cases.DTO;

public class LoginDTO : Request
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&])(?=.*[\d])([A-Za-z!@#$%&\d]){8,}$",
        ErrorMessage = "o campo precisa de 8 caracteres, 1 caractere maiúsculo, minúsculo, um número e um caractere especial" )]
    public string UserPassword { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; } = String.Empty;

    public List<int>? Roles { get; set; }
}