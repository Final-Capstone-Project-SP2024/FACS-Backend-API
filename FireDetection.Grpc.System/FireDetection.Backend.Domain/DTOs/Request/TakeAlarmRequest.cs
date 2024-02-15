using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class TakeAlarmRequest
    {
   
        public  decimal PredictedPercent { get; set; } = 0;


        public string PictureUrl { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;
    }
}
