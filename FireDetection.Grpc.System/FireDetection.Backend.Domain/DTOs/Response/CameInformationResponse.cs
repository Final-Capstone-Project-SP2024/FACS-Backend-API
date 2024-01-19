using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class CameInformationResponse
    {
        public Guid CameraId { get; set; }
        public string Status { get; set; } = null!;
        public string CameraDestination { get; set; } = null!;
    }
}
