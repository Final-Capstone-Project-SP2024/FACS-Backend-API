﻿
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
                ActionName = "Alarm Level 1",
                ActionDescription = "Small fire can be extinguished immediately",
                Min = 0,
                Max = 60,
            }, new ActionType
            {
                ID = 2,
                ActionName = "Alarm Level 2",
                ActionDescription = "the fire needs to mobilize more people in the nearby area",
                Min = 60,
                Max = 70

            },
          new ActionType
          {
              ID = 3,
              ActionName = "Alarm Level 3",
              ActionDescription = "a large fire can affect and cause damage, mobilizing everyone",
              Min = 70,
              Max = 80, 

          }
          ,
           new ActionType
           {
               ID = 4,
               ActionName = "Alarm Level 4",
               ActionDescription = "a large fire can affect and cause damage, mobilizing everyone",
               Min= 80,
               Max = 90,

           }
             ,
           new ActionType
           {
               ID = 5,
               ActionName = "Alarm Level 5",
               ActionDescription = "a large fire can affect and cause damage, mobilizing everyone",
               Min = 90,
               Max = 100,
           },
          new ActionType
          {
              ID = 6,
              ActionName = "End Action",
              ActionDescription = "a large fire can affect and cause damage, mobilizing everyone"
          }
          ,
          new ActionType
          {
              ID = 7,
              ActionName = "Fake  Alarm",
              ActionDescription = ""
          }
          ,
          new ActionType
          {
              ID = 8,
              ActionName = "Repair the camera",
              ActionDescription = "AI model is disconnected from the camera"
          }
          );

            builder.HasMany(x => x.RecordProcesses).WithOne(x => x.ActionType).HasForeignKey(x => x.ActionTypeId);
        }
    }
}
