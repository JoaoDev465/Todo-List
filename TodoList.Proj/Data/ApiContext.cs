using Apicontext.File.FluentApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;

namespace Apicontext.File;

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
