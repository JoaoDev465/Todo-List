using TodoList.Proj.Models.user;

namespace TodoList.Proj.Models;
// class for represent users tasks
public class Todo
{
    public int Id{ get; set; }
    public bool Start { get; set; }
    public DateTime Initialized { get; set; }
    public string Task { get; set; } = String.Empty;
    public string? Description { get; set; }
   
    public DateTime? Alert { get; set; }
    public bool Finalized{ get; set; }

    public int UserId { get; set; }
    public User User { get; set; } 
}