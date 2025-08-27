using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Uses_Cases.DTO;

// class that work  to padronize errors and  forms
public class UserDto : Request
{
    public int Id { get; set; }
    [Required]
    [EmailValidation]
    public string UserEmail { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "o campo senha é obrigatório")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&])[A-Za-z\d!@#$%&]{12,}$",
        ErrorMessage = "a senha precisa conter pelo menos 1 letra maiúscula, 1 minúscula, 1 caractere especial ex: '@!$%&#', e 12 caracteres ")]
    public string  UserPassword { get; set; } = String.Empty;
    
    public string Slug { get; set; } = string.Empty;
    
    
}

public class EmailValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = value as string;

        if (string.IsNullOrWhiteSpace(email))
        {
            return new ValidationResult("o campo não pode ser nulo");
        }
        else if (!email.EndsWith("@gmail.com"))
        {
            return new ValidationResult("o campo precisa conter '@gmail.com'");
        }
        return ValidationResult.Success;
    }
    
}