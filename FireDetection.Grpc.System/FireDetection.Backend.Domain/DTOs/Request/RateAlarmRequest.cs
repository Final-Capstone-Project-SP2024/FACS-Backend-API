using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class RateAlarmRequest
    {


        [Range(0,5)]
        public int LevelRating { get; set; }

  
    }
}
