using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Proj.Models;
// this class define the role user,simple and easy by implement in Db
public class Role 
{
    
    public int Id { get; set; }
    public string  Name { get; set; } = String.Empty;
    public string Slug { get; set; } = string.Empty;
   public IList<User>Users { get; set; }
}