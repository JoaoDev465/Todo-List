using System.ComponentModel.DataAnnotations;

namespace TodoListCore.Uses_Cases.DTO;

public class TodoDto : Request
{
    public int Id { get; set; }

    [Required(ErrorMessage = "o campo tarefa é obrigatório")]
    public string Task { get; set; } = string.Empty;

    public string? DescriptionOfTask { get; set; } = string.Empty;
  
}