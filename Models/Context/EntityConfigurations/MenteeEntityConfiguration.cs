using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class MenteeEntityConfiguration : IEntityTypeConfiguration<Mentee>
    {
        public void Configure(EntityTypeBuilder<Mentee> builder)
        {
            builder.HasOne(x => x.User)
            .WithOne(x => x.Mentee)
            .HasForeignKey<Mentee>(x => x.UserId);

            builder.HasOne(x => x.Mentor)
            .WithMany(x => x.Mentees)
            .HasForeignKey(x => x.MentorId).IsRequired(false);

            builder.HasMany(x => x.Meetings)
            .WithOne(x => x.Mentees);
        }
    }
}