using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class DetectResponse
    {
        public Guid CameraId { get; set; }

        public string RecordId { get; set; }

        public DateTime Createdate { get; set; }
        public string Status {  get; set; }
    
       
    }
}
