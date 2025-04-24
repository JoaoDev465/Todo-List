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
    // this row , work for define the time by remeber some Task that you bookmarked to later
    public DateTime Alert { get; set; }
    public bool finalized{ get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = new();
}