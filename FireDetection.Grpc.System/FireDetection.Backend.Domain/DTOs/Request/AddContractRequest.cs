using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddContractRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
        public bool IsPaid { get; set; }

        public decimal TotalPrice { get; set; }

        public string ContractImage { get; set; }

        public int ManualPlanType { get; set; }
        public Guid UserId { get; set; }


    }
}
