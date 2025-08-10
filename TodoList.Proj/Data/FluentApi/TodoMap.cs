using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListCore.Models;

namespace TodoList.Proj.Data.FluentApi;

// this class maps todos to DB
public class TodoMap:IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable("Tasks");

        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();
        

        builder.Property(x => x.Task)
            .IsRequired()
            .HasColumnName("Task")
            .HasColumnType("Nvarchar")
            .HasMaxLength(1000);

        builder.Property(x => x.Description)
            .HasColumnName("DEscription_Task")
            .HasColumnType("Nvarchar")
            .HasMaxLength(2500);

      
    }
}