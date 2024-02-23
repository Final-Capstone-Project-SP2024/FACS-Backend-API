using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class RecordRequest
    {
        public Guid CameraId { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
    }
}
