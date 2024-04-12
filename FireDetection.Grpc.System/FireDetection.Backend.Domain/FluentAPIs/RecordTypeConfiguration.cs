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
    public class RecordTypeConfiguration : IEntityTypeConfiguration<RecordType>
    {
        public void Configure(EntityTypeBuilder<RecordType> builder)
        {
            builder.HasData(new RecordType
            {
                RecordTypeId = 1,
                Name = "Detection"
            },
             new RecordType
             {
                 RecordTypeId = 2,
                 Name = "ElectricalIncident"
             },
             new RecordType
             {
                 RecordTypeId = 3,
                 Name = "AlarmByUser"
             });
            builder.HasMany(x => x.Records).WithOne(x => x.RecordType).HasForeignKey(x => x.RecordTypeID);
        }
    }
}
