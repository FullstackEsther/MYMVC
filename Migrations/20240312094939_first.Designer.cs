﻿// <auto-generated />
using System;
using MYMVC.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MYMVC.Migrations
{
    [DbContext(typeof(MvcContext))]
    [Migration("20240312094939_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MYMVC.Models.Community", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CommunityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Communities");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Chat", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Meeting", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MenteeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MentorId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MenteeId");

                    b.HasIndex("MentorId");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MentorId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MentorId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Mentees");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ChatContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ChatId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SenderUserName")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<TimeSpan>("TimeSent")
                        .HasColumnType("TIME");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Profile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("dateTime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            Id = "8f4667b3-9f21-42b7-80a0-7e3eaf5cdf97",
                            Address = "abk",
                            Age = 4,
                            DateCreated = new DateTime(2024, 3, 12, 10, 49, 38, 832, DateTimeKind.Local).AddTicks(6352),
                            FirstName = "Esther",
                            LastName = "Otufale",
                            PhoneNumber = "090876543213",
                            UserId = "9a07d60f--4930-8e8b1629"
                        });
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "8f4667b3-9f21-42b7-80a0",
                            DateCreated = new DateTime(2024, 3, 12, 10, 49, 38, 832, DateTimeKind.Local).AddTicks(3065),
                            Description = "Application overseer",
                            RoleName = "SuperAdmin"
                        });
                });

            modelBuilder.Entity("MYMVC.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CommunityId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "9a07d60f--4930-8e8b1629",
                            DateCreated = new DateTime(2024, 3, 12, 10, 49, 38, 830, DateTimeKind.Local).AddTicks(866),
                            Email = "otufalesther@gmail.com",
                            Password = "12",
                            RoleId = "8f4667b3-9f21-42b7-80a0",
                            UserName = "Esther"
                        });
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Meeting", b =>
                {
                    b.HasOne("MYMVC.Models.Entities.Mentee", "Mentees")
                        .WithMany("Meetings")
                        .HasForeignKey("MenteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MYMVC.Models.Entities.Mentor", "Mentor")
                        .WithMany("Meetings")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentees");

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentee", b =>
                {
                    b.HasOne("MYMVC.Models.Entities.Category", "Category")
                        .WithMany("Mentee")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MYMVC.Models.Entities.Mentor", "Mentor")
                        .WithMany("Mentees")
                        .HasForeignKey("MentorId");

                    b.HasOne("MYMVC.Models.Entities.User", "User")
                        .WithOne("Mentee")
                        .HasForeignKey("MYMVC.Models.Entities.Mentee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Mentor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentor", b =>
                {
                    b.HasOne("MYMVC.Models.Entities.Category", "Category")
                        .WithMany("Mentor")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MYMVC.Models.Entities.User", "User")
                        .WithOne("Mentor")
                        .HasForeignKey("MYMVC.Models.Entities.Mentor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Message", b =>
                {
                    b.HasOne("MYMVC.Models.Entities.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Profile", b =>
                {
                    b.HasOne("MYMVC.Models.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("MYMVC.Models.Entities.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.User", b =>
                {
                    b.HasOne("MYMVC.Models.Community", "Community")
                        .WithMany("CommunityMembers")
                        .HasForeignKey("CommunityId");

                    b.HasOne("MYMVC.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MYMVC.Models.Community", b =>
                {
                    b.Navigation("CommunityMembers");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Category", b =>
                {
                    b.Navigation("Mentee");

                    b.Navigation("Mentor");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentee", b =>
                {
                    b.Navigation("Meetings");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Mentor", b =>
                {
                    b.Navigation("Meetings");

                    b.Navigation("Mentees");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MYMVC.Models.Entities.User", b =>
                {
                    b.Navigation("Mentee")
                        .IsRequired();

                    b.Navigation("Mentor")
                        .IsRequired();

                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
