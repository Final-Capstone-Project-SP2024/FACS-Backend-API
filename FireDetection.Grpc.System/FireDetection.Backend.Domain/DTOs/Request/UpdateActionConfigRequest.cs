using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UpdateActionConfigRequest
    {
        public decimal? Min {  get; set; }  

        public decimal? Max { get; set; }
    }
}
