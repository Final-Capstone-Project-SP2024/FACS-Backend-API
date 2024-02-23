using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class RecordResponse
    {
        public Guid CameraId { get; set; }
        public List<RecordFollows>? RecordFollows { get; set; } = new List<RecordFollows>();
    }

    public class RecordFollows
    {
        public Guid RecordId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int RecordTypeId { get; set; }
    }


}
