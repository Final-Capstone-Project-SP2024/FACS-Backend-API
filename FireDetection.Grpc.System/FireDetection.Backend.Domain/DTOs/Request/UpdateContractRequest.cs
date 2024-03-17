using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UpdateContractRequest
    {
        public DateTime EndDate { get; set; }

        public bool IsPaid { get; set; }

        public decimal TotalPrice { get; set; }
        public int ManualPlanId { get; set; }
    }
}
