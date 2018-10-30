using Domain.Common.Configurations;
using Domain.Models;
using Domain.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contexts
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Lock> Locks { get; set; }
        public virtual DbSet<LockOperation> LockOperations { get; set; }
        public virtual DbSet<LockRent> LockRents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LockConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LockOperationConfiguration());
            modelBuilder.ApplyConfiguration(new LockRentConfiguration());
        }
    }
}
