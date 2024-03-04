using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class LocationGetAllResponse
    {
        public Guid LocationId { get; set; }

        public int  CameraQuantity { get; set; } = 0;

        public int UserQuantity { get; set; } = 0;

        public DateTime CreatedDate { get; set; }

    }
}
