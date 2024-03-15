using FireDetection.Backend.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.FluentAPIs
{
    public class UserTransactionConfiguration : IEntityTypeConfiguration<UserTransaction>
    {
        public void Configure(EntityTypeBuilder<UserTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.User).WithOne(x => x.UserTransaction).HasForeignKey<UserTransaction>(x => x.UserID);
            builder.HasMany(x =>  x.Transaction).WithOne(x => x.UserTransaction).HasForeignKey(x => x.UserTransactionID);

        }
    }
}
