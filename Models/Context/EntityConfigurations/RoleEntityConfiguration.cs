using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(x => x.Users)
            .WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.HasData
            (
                new Role
                {
                    RoleName = "SuperAdmin",
                    Description = "Application overseer",
                    Id = "8f4667b3-9f21-42b7-80a0",
                    DateCreated = DateTime.Now,
                }
            );
        }
    }
}