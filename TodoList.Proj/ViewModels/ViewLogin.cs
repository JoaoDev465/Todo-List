﻿using System.ComponentModel.DataAnnotations;

namespace View.ViewModels;

public class ViewLogin
{
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserPassword { get; set; }
    
    [Required(ErrorMessage = "o campo é obrigatório")]
    public string UserEmail { get; set; }
}