using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class TakeAlarmRequest
    {
        public string CameraId { get; set; } = null!;
        public  decimal PredictedPercent { get; set; } = 0;
        public string PercentFire { get; set; } = null!;
        public string Time { get; set; } = null!;

        public string PictureUrl = null!;

        public string VideoUrl = null!;
    }
}
