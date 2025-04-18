using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Proj.Models;
using TodoList.Proj.Models.Roles;

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
        
    }
}