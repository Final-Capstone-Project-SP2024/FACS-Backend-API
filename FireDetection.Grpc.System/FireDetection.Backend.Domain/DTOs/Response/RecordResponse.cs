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
        public List<CameraFollow> cameraFollow { get; set; }
    }

    public class CameraFollow
    {
        public Guid RecordId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int RecordTypeId { get; set; }
    }


}
