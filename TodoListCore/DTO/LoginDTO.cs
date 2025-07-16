using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TodoListCore;
using ViewModels.User;

namespace View.ViewModels;

public class LoginDTO : Request
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%&])(?=.*[\d])([A-Za-z!@#$%&\d]){12,}$")]
    public string UserPassword { get; set; }
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; }

    public List<int> Roles { get; set; }
}