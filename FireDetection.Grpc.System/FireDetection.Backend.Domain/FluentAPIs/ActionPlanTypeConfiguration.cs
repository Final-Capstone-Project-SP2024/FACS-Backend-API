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
    public class ActionPlanTypeConfiguration : IEntityTypeConfiguration<ActionPlanType>
    {
        public void Configure(EntityTypeBuilder<ActionPlanType> builder)
        {
            builder.HasMany(x => x.Transactions).WithOne(x => x.ActionPlanType).HasForeignKey(x => x.ActionPlanTypeID);
            builder.HasData(new ActionPlanType
            {
                ActionPlanTypeId = 1,
                ActionPlanTypeName = "Upgrade",
            },
            new ActionPlanType
            {
                ActionPlanTypeId = 2,
                ActionPlanTypeName = "Downgrade",
            },
            new ActionPlanType
            {
                ActionPlanTypeId = 3,
                ActionPlanTypeName = "Renewal",
            });
        }
    }
}
