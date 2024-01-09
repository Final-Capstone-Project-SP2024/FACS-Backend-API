using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class MediaRecord : BaseEntity
    {
        public string Url { get; set; } = null!;

        public Record Record { get; set; } = null!;

        public Guid RecordId { get; set; }


        public MediaType MediaType { get; set; } = null!;

        public int MediaTypeId { get; set; }

    }
}
