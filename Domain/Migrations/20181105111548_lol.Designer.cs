﻿// <auto-generated />
using System;
using Domain.Common.Enums;
using Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181105111548_lol")]
    partial class lol
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Lock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Locked");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Locks");
                });

            modelBuilder.Entity("Domain.Models.LockOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime");

                    b.Property<int>("LockId");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("LockId");

                    b.ToTable("LockOperations");
                });

            modelBuilder.Entity("Domain.Models.LockRent", b =>
                {
                    b.Property<int>("LockId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("RentStart");

                    b.Property<DateTime?>("RentFinish");

                    b.Property<int>("Rights");

                    b.HasKey("LockId", "UserId", "RentStart");

                    b.HasIndex("UserId");

                    b.ToTable("LockRents");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(2);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.LockOperation", b =>
                {
                    b.HasOne("Domain.Models.Lock", "Lock")
                        .WithMany("LockOperations")
                        .HasForeignKey("LockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Models.LockRent", b =>
                {
                    b.HasOne("Domain.Models.Lock", "Lock")
                        .WithMany("LockRents")
                        .HasForeignKey("LockId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany("LockRents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
