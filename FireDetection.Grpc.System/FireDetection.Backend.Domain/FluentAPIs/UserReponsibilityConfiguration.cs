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
    public class UserReponsibilityConfiguration : IEntityTypeConfiguration<UserReponsibility>
    {
        public void Configure(EntityTypeBuilder<UserReponsibility> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(userConfig => userConfig.Id).ValueGeneratedOnAdd();
        }
    }
}
