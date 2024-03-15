using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class MentorEntityConfiguration : IEntityTypeConfiguration<Mentor>
    {
        public void Configure(EntityTypeBuilder<Mentor> builder)
        {
            builder.HasOne(x => x.User)
            .WithOne(x => x.Mentor)
            .HasForeignKey<Mentor>(x => x.UserId);

            builder.HasMany(x => x.Mentees)
            .WithOne(x => x.Mentor);
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Meetings)
            .WithOne(x => x.Mentor);
            
        }
    }
}