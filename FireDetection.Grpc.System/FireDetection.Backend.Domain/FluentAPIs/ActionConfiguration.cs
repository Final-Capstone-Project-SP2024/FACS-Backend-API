
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
    public class ActionConfiguration : IEntityTypeConfiguration<ActionType>
    {
        public void Configure(EntityTypeBuilder<ActionType> builder)
        {
            builder.HasData(new ActionType
            {
                ID = 1,
                ActionName = "action",
                ActionDescription = "actiondes",
            }, new ActionType
            {
                ID = 2,
                ActionName = "action",
                ActionDescription = "actiondes"
            },
          new ActionType
          {
              ID = 3,
              ActionName = "action",
              ActionDescription = "actiondes"
          }

          );

            builder.HasMany(x => x.RecordProcesses).WithOne(x => x.ActionType).HasForeignKey(x => x.ActionTypeId);
        }
    }
}
