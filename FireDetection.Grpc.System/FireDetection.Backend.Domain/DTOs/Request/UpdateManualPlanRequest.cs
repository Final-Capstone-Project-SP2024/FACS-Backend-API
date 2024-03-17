using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UpdateManualPlanRequest
    {
        [Range(1,100)]
        public int LocationLimited { get; set; }

        [Range(1, 100)]
        public int CameraLimited { get; set; }
        [Range(1, 100)]
        public int UserLimited { get; set; }

        [Range(1,1000000)]
        public decimal Price {  get; set; }
    }
}
