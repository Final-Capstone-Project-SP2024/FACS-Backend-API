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
    public class AlarmConfigConfiguration : IEntityTypeConfiguration<AlarmConfiguration>
    {
        public void Configure(EntityTypeBuilder<AlarmConfiguration> builder)
        {
            builder.HasKey(x => x.AlarmConfigurationId);
            builder.HasMany(x => x.Records).WithOne(x => x.AlarmConfiguration).HasForeignKey(x => x.AlarmConfigurationId);
            builder.HasData(new AlarmConfiguration
            {
                AlarmConfigurationId = 1,
                Start = 0,
                End = 20,
                AlarmNameConfiguration = "Fake Alarm",
                NumberOfNotification = 0,
            },
            new AlarmConfiguration
            {
                AlarmConfigurationId = 2,
                Start = 20,
                End = 60,
                AlarmNameConfiguration =  "Small Fire",
                NumberOfNotification = 1
            }, new AlarmConfiguration
            {
                AlarmConfigurationId = 3,
                Start = 60,
                End = 40,
                AlarmNameConfiguration = "Big Fire",
                NumberOfNotification = 99
            }) ;
        }
    }
}
