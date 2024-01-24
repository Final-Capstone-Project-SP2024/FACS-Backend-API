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
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.MediaRecords).WithOne(x => x.Record).HasForeignKey(x => x.RecordId);
            builder.HasMany(x => x.RecordProcesses).WithOne(x => x.Record).HasForeignKey(x => x.RecordID);
            builder.HasMany(x => x.AlarmRates).WithOne(x => x.Record).HasForeignKey(x => x.RecordID);
         
        }
    }
}
