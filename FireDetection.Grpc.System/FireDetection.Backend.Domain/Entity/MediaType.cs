using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class MediaType 
    {
        public int MediaTypeID { get; set; }
        public string MediaName { get; set; } = null!;

        public ICollection<MediaRecord>? MediaRecords { get; set;}
    }
}
