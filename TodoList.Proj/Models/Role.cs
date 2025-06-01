using TodoList.Proj.Models.user;

namespace TodoList.Proj.Models.Roles;
// this class define the role user,simple and easy by implement in Db
public class Role
{
    public int Id { get; set; }
    public string  Name { get; set; } = String.Empty;
    
    public List<User> Users { get; set; } = new();
   
}