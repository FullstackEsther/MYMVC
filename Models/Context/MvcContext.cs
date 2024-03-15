using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MYMVC.Models.Context.EntityConfigurations;
using MYMVC.Models.Entities;

namespace MYMVC.Models.Context
{
    public class MvcContext : DbContext
    {
        public MvcContext(DbContextOptions options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
           modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
           modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
           modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
           modelBuilder.ApplyConfiguration(new MentorEntityConfiguration());
           modelBuilder.ApplyConfiguration(new MenteeEntityConfiguration());
           modelBuilder.ApplyConfiguration(new MeetingEntityConfiguration());
           modelBuilder.ApplyConfiguration(new CommunityEntityConfiguration());
           modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
           modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());

            
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<Profile> Profiles {get; set;}
        public DbSet<Message> Messages {get; set;}
        public DbSet<Mentee> Mentees {get; set;}
        public DbSet<Mentor> Mentors {get; set;}
        public DbSet<Community> Communities {get; set;}
        public DbSet<Meeting> Meetings {get; set;}
        public DbSet<Chat> Chats {get; set;}
        public DbSet<Category> Categories {get; set;}
    }
}