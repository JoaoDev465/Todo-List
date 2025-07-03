using System.ComponentModel.DataAnnotations;
using TodoListCore;

namespace View.ViewModels;

public class LoginDTO : Request
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserPassword { get; set; }
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; }

    public List<int> Roles { get; set; }
}