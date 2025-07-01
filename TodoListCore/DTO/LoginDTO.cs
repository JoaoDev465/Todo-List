using System.ComponentModel.DataAnnotations;

namespace View.ViewModels;

public class LoginDTO
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserPassword { get; set; }
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; }

    public List<int> Roles { get; set; }
}