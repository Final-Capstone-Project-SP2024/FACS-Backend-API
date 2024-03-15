using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }

        public string PaymentTypeName { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
