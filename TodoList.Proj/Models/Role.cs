namespace TodoList.Proj.Models;
// this class define the role user,simple and easy by implement in Db
public class Role
{
    public int Id { get; set; }
    public string  Name { get; set; } = String.Empty;

    public int UserId { get; set; }
    public List<User> Users { get; set; } = new();
}