using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class TransactionGeneralResponse
    {
        public Guid TransactionId { get; set; }
        public decimal Price { get; set; }
        public bool isPaid { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class TransactionDetailResponse : TransactionGeneralResponse
    {
        public Guid UserID { get; set; }
        public Guid ContractID { get; set; }
        public int ActionPlanTypeID { get; set; }
        public int PaymentTypeID { get; set; }
    }
}
