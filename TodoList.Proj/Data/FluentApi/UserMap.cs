using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;

namespace Apicontext.File.FluentApi;

// this is class for User maps statements to Data Base
public class UserMap: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("Nvarchar")
            .HasMaxLength(200);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("Nvarchar")
            .HasMaxLength(200);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("Nvarchar")
            .HasMaxLength(350);

        builder.Property(x => x.IsOnline)
            .HasDefaultValue(false);

        // relationship many for many
        builder
            .HasMany(x => x.Roles)
            .WithMany(x => x.users)
            .UsingEntity<Dictionary<string, object>>(
                "RoleUser",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_RoleUser_UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                tag => tag
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_RoleUser_RoleId")
                    .OnDelete(DeleteBehavior.Cascade));

        // relationship many for one
        builder.HasMany(x => x.Todos)
            .WithOne(x => x.User)
            .HasConstraintName("FK_TodoUser")
            .OnDelete(DeleteBehavior.Cascade);
    }
}