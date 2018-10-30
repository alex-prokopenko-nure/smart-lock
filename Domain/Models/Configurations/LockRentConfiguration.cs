using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Configurations
{
    class LockRentConfiguration : IEntityTypeConfiguration<LockRent>
    {
        public void Configure(EntityTypeBuilder<LockRent> builder)
        {
            builder.HasKey(x => new { x.LockId, x.UserId, x.RentStart });

            builder.HasOne(x => x.Lock)
                .WithMany(y => y.LockRents)
                .HasForeignKey(z => z.LockId);

            builder.HasOne(x => x.User)
                .WithMany(y => y.LockRents)
                .HasForeignKey(z => z.UserId);
        }
    }
}
