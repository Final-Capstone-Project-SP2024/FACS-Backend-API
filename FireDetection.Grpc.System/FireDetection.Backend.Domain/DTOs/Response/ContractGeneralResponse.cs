using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class ContractGeneralResponse
    {
        public Guid ContractID { get; set; }
        public bool isActive { get; set; }
        public bool isPaid { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ContractDetailResponse : ContractGeneralResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ManualTypeID { get; set; }
    }
}
