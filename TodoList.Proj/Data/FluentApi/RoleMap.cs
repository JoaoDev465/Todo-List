﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Proj.Models;

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
            .HasColumnName("Role_Name")
            .HasColumnType("Nvarchar")
            .HasMaxLength(100);
        builder.Property(x => x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("Nvarchar")
            .HasMaxLength(100);

    }
}