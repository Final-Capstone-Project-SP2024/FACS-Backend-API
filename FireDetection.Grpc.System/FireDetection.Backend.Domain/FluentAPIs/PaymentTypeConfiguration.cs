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
    public class PaymentTypeConfiguration : IEntityTypeConfiguration<PaymentType>
    {
        public void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.HasMany(x => x.Transactions).WithOne(x => x.PaymentType).HasForeignKey(x => x.PaymentTypeID);
            builder.HasData(new PaymentType
            {
                PaymentTypeId = 1,
                PaymentTypeName = "DirectPayment"
            },
            new PaymentType
            {
                PaymentTypeId = 2,
                PaymentTypeName = "OnlinePayment"
            });
        }
    }
}
