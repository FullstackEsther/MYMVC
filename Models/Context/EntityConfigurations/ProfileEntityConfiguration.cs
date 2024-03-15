using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasOne(x => x.User).WithOne(x => x.Profile).HasForeignKey<Profile>(x => x.UserId);
            builder.Property(x => x.FirstName).HasMaxLength(15);
            builder.Property(x => x.LastName).HasMaxLength(15);
            builder.Property(x => x.LastName).HasMaxLength(15);
            builder.Property(x => x.DateCreated).HasColumnType("dateTime");
            builder.HasData(
                 new Profile
                 {
                     Id = "8f4667b3-9f21-42b7-80a0-7e3eaf5cdf97",
                     Address = "abk",
                     Age = 4,
                     FirstName = "Esther",
                     LastName = "Otufale",
                     PhoneNumber = "090876543213",
                     DateCreated = DateTime.Now,
                      UserId ="9a07d60f--4930-8e8b1629"
                 }
            );



        }
    }
}