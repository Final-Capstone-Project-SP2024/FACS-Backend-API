using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UpdateActionConfigRequest
    {

        [Range(0,100)]
        public decimal? Min {  get; set; }

        [Range(0,100)]
        public decimal? Max { get; set; }
    }
}
