using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddTransactionRequest
    {
        public bool IsPaid { get; set; }

        public int PaymentType { get; set; }

    }
}
