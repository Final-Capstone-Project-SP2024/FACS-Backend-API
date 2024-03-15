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
    public class ManualPlanConfiguration : IEntityTypeConfiguration<ManualPlan>
    {
        public void Configure(EntityTypeBuilder<ManualPlan> builder)
        {
            builder.HasData(new ManualPlan
            {
             ManualPlanNameId = 1,
             ManualPlanName = "Basic",
             CameraLimited = 4,
             LocationLimited = 5,
             Price = 1000,
             UserLimited = 5,
             CreatedDate = DateTime.UtcNow,
             IsDeleted = false,
             
            }, new ManualPlan
            {
                ManualPlanNameId = 2,
                ManualPlanName = "Standard",
                CameraLimited = 8,
                LocationLimited = 10,
                Price = 2000,
                UserLimited = 10,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            }, new ManualPlan
            {
                ManualPlanNameId = 3,
                ManualPlanName = "Premium",
                CameraLimited = 20,
                LocationLimited = 20,
                Price = 3000,
                UserLimited = 20,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            });

            builder.HasMany(x => x.UserTransaction).WithOne(x => x.ManualPlan).HasForeignKey(x => x.ManualPlanID);

        }
    }
}
