namespace TodoListCore.Models;

public class User
{
   
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash{ get; set; } = String.Empty;
    public List<Todo> Todos { get; set; } = new();
    
    public IList<Role> Roles { get; set; } 
}