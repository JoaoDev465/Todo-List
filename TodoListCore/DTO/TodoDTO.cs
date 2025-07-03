using System.ComponentModel.DataAnnotations;
using TodoListCore;

namespace ViewModels.Todo;

public class TodoDTO : Request
{
    public int TaskId { get; set; }
    public bool Start_Task { get; set; }
    public DateTime InitializeDateTimeTask { get; set; }
    [Required(ErrorMessage = "o campo tarefa é obrigatório")]
    public string Task { get; set; } = string.Empty;
    public string? DescriptionOfTask { get; set; } = string.Empty;
    public DateTime AlertForDateTask { get; set; }
    public bool FinalizedTimeTask { get; set; }
    public int userId { get; set; }
}