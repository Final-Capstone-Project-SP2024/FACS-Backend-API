using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddCameraRequest
    {
        public string Status { get; set; }= null!;

        public string Destination { get; set; }= null!;
    }
}
