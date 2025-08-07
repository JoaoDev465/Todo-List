using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Data.FluentApi;
using TodoList.Proj.Models;
using TodoListCore.Models;

namespace TodoList.Proj.Data;

public class  Context:DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)

    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new RoleMap());
        modelBuilder.ApplyConfiguration(new TodoMap());
    }

   
}
