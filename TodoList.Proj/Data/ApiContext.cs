using Microsoft.EntityFrameworkCore;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;

namespace Apicontext.File;

class  Context:DbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)

    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Role> Roles { get; set; }
}
