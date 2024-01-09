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
    public class MediaTypeConfiguration : IEntityTypeConfiguration<MediaType>
    {
        public void Configure(EntityTypeBuilder<MediaType> builder)
        {
            builder.HasData(new MediaType
            {
                MediaTypeID = 1,
                MediaName = "video"
            },
            new MediaType
            {
                MediaTypeID = 2,
                MediaName = "image"
            });
        }
    }
}
