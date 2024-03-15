using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class MeetingEntityConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasOne(x => x.Mentees)
            .WithMany(x => x.Meetings)
            .HasForeignKey(x => x.MenteeId);

            builder.HasOne(x => x.Mentor)
            .WithMany(x => x.Meetings)
            .HasForeignKey(x => x.MentorId);
        }
    }
}