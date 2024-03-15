using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context.EntityConfigurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(x => x.Mentor)
            .WithOne(x => x.Category);

            builder.HasMany(x => x.Mentee)
            .WithOne(x => x.Category);
            
            builder.Property(x => x.Description).HasMaxLength(250);
        }
    }
}