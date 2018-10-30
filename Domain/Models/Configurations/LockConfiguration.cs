using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Configurations
{
    class LockConfiguration : IEntityTypeConfiguration<Lock>
    {
        public void Configure(EntityTypeBuilder<Lock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.LockOperations)
                .WithOne(y => y.Lock);
        }
    }
}
