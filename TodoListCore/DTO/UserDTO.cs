using System.Collections.Frozen;
using System.ComponentModel.DataAnnotations;
using ViewModels.Role;


namespace ViewModels.User;

// class that work  to padronize errors and  forms
public class UserDto
{
    public int? Id { get; set; }
    [Required(ErrorMessage = "O campo é obrigatório")]
    [MinLength(12,ErrorMessage = "esse campo precisa de no mínimo 12 caracteres"),MaxLength(50)]
    public string UserName { get; set; } = string.Empty;
    [Required]
    [EmailValidation]
    public string UserEmail { get; set; } = String.Empty;
    [Required(ErrorMessage = "o campo senha é obrigatório")]
    // used patterns and regular expression for padronize passowords
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&])[A-Za-z\d!@#$%&]{12,}$")]
    public string  UserPassword { get; set; } = String.Empty;
    public bool UserAreOnline { get; set; }
    
    public string Slug { get; set; } = string.Empty;
    
}

public class EmailValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;

        if (string.IsNullOrWhiteSpace(email))
        {
            ErrorMessage = "o campo não pode ser nulo";
        }
        else if (!email.EndsWith("@gmail.com"))
        {
            ErrorMessage = "o email precisa pertencer ao dominio '@gmail.com'";
        }
        return ValidationResult.Success;
    }
    
}