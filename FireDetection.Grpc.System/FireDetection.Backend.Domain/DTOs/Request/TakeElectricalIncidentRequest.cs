using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class TakeElectricalIncidentRequest
    {
        public Guid CameraID { get; set; }
        public string Time {  get; set; }
    }
}
