using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class TakeAlarmRequest
    {

        [Range(1,100)]
        public  decimal PredictedPercent { get; set; } = 0;


        public string PictureUrl { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;
    }
}
