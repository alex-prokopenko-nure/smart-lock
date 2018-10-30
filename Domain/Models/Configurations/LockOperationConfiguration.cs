using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Configurations
{
    class LockOperationConfiguration : IEntityTypeConfiguration<LockOperation>
    {
        public void Configure(EntityTypeBuilder<LockOperation> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
