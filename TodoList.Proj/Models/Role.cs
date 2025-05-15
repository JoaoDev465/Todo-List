using TodoList.Proj.Models.user;

namespace TodoList.Proj.Models.Roles;
// this class define the role user,simple and easy by implement in Db
public class Role
{
    public IEnumerable<User>? users;
    public int Id { get; set; }
    public string  Name { get; set; } = String.Empty;

    public int UserId { get; set; }
    public List<User> Users { get; set; } = new();
}