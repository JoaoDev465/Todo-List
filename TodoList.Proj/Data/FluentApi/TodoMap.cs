using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Proj.Models;

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

        // checkup to see if user are online or not
        builder.Property(x => x.Start)
            .HasColumnName("IsOnline")
            .HasDefaultValue(false);

        builder.Property(x => x.Initialized)
            .HasColumnName("initialized_time")
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.Task)
            .IsRequired()
            .HasColumnName("Task")
            .HasColumnType("Nvarchar")
            .HasMaxLength(1000);

        builder.Property(x => x.Description)
            .HasColumnName("DEscription_Task")
            .HasColumnType("Nvarchar")
            .HasMaxLength(2500);

        builder.Property(x => x.Finalized)
            .HasColumnName("Finalized_Time")
            .HasDefaultValue(false);
    }
}