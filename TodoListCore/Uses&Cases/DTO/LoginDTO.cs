using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Uses_Cases.DTO;

public class LoginDTO : Request
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&])(?=.*[\d])([A-Za-z!@#$%&\d]){12,}$")]
    public string UserPassword { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; } = String.Empty;

    public List<int>? Roles { get; set; }
}