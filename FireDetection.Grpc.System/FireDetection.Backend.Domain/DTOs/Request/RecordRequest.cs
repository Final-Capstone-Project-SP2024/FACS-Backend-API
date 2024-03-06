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
        public Guid RecordId { get; set; }
        public Guid CameraId { get; set; }
        public Guid LocationId { get; set; }
        public string? Status { get; set; }
        public DateTime FromDate { get; set; } = DateTime.UtcNow.AddDays(-7); //default 7 days ago
        public DateTime ToDate { get; set; } = DateTime.UtcNow;
    }
}
