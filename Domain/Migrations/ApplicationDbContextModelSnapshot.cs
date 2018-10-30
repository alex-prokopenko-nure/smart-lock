﻿// <auto-generated />
using System;
using Domain.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Lock");
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

                    b.ToTable("LockOperation");
                });

            modelBuilder.Entity("Domain.Models.LockRent", b =>
                {
                    b.Property<int>("LockId");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("RentStart");

                    b.Property<DateTime>("RentFinish");

                    b.HasKey("LockId", "UserId", "RentStart");

                    b.HasIndex("UserId");

                    b.ToTable("LockRent");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

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
