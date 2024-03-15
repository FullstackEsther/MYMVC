using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.Community).
                WithMany(x => x.CommunityMembers).
                HasForeignKey(x => x.CommunityId).IsRequired(false);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.Mentor)
            .WithOne(x => x.User);
            builder.Property(x => x.UserName).HasMaxLength(12);
            builder.Property(x => x.Password).HasMaxLength(8);
            builder.HasKey(x => x.Id);
            builder.HasData(
                new User
                {
                    Id = "9a07d60f--4930-8e8b1629",
                    Email = "otufalesther@gmail.com",
                    Password = "12",
                    UserName = "Esther",
                    RoleId = "8f4667b3-9f21-42b7-80a0"
                }
            );
        }
    }
}