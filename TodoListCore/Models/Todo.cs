namespace TodoList.Proj.Models;
// class for represent users tasks
public class Todo
{
    public int Id{ get; set; }
  
    public string Task { get; set; } = String.Empty;
    public string? Description { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } 
}