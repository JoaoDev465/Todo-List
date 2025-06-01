using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;
using TodoList.Proj.Models.user;

namespace Apicontext.File.FluentApi;
// this class maps Roles to Db
public class RoleMap: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Role_Name")
            .HasColumnType("Nvarchar")
            .HasMaxLength(100);

        builder.HasMany(x => x.Users)
            .WithMany(x => x.Roles)
            .UsingEntity<Dictionary<string, object>>
            (
                "UserRole_Role",
                role => role
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .HasConstraintName("FK_UsersRole_RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .HasConstraintName("FK_RolesUser_UserId")
                    .OnDelete(DeleteBehavior.Cascade)
            );


    }
}