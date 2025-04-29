using TodoList.Proj.Models.Roles;

namespace TodoList.Proj.Models.user;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash{ get; set; } = String.Empty;
    public bool IsOnline{ get; set; }

    public int TodoId{ get; set; }
    public List<Todo> Todos { get; set; } = new();

    public int RoleId { get; set; }
    public List<Role> Roles { get; set; }
}