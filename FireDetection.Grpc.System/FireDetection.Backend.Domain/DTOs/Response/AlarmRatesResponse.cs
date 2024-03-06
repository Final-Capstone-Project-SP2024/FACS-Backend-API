using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class AlarmRatesResponse
    {
        public Guid UserId { get; set; }
        public int Rating { get; set; }
    }
}
