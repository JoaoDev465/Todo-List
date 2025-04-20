using System.ComponentModel.DataAnnotations;

namespace ViewModels.Todo;

public class ViewTodo
{
    public bool Start { get; set; }
    public DateTime Initialized { get; set; }
    [Required(ErrorMessage = "o campo tarefa é obrigatório")]
    public string Task { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Alert { get; set; }
    public bool Finalized { get; set; }
}