using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class NotificationAlarmResponse
    {
        public  Guid RecordId { get; set; }
        public Guid CameraId { get; set; }
        public string LocationName { get; set; }
        public string CameraName { get; set; }
        public string CameraDestination { get; set; }
        public string Status {  get; set; }
        public string occurrenceTime { get; set; } = string.Empty;    
        public  int RecordType { get; set; }



    }
}
