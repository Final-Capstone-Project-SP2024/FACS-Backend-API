using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class ManualPlanGeneralResponse
    {
        public int ManualPlanId { get; set; }

        public string ManualPlanName { get; set; }
    }


    public class ManualPlanDetailResponse : ManualPlanGeneralResponse
    {
        public int LocationLimited { get; set; }

        public int CameraLimited { get; set; }

        public int UserLimited { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
