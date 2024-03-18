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
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasData(new Level
            {
                LevelID = 1,
                Description = "Small Fire",
                Name = "Level 1"
            },
            new Level
            {
                LevelID = 2,
                Description = "Fire ",
                Name = "Level 2"
            },
             new Level
             {
                 LevelID = 3,
                 Description = "Fire ",
                 Name = "Level 3"
             },
              new Level
              {
                  LevelID = 4,
                  Description = "Fire ",
                  Name = "Level 4"
              },
               new Level
               {
                   LevelID = 5,
                   Description = "Fire ",
                   Name = "Level 5"
               },
               new Level
               {
                   LevelID = 6,
                   Description = "Fake Alarm",
                   Name = "Fake Alarm"
               }
            );
            builder.HasMany(x => x.AlarmRates).WithOne(x => x.Level).HasForeignKey(x => x.LevelID);
        }
    }
}
