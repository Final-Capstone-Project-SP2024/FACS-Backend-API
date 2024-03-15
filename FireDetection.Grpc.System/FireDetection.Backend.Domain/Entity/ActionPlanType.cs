using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class ActionPlanType
    {
        [Key]
        public int ActionPlanTypeId { get; set; }

        public string? ActionPlanTypeName { get; set;}

        public ICollection<Transaction> Transactions { get; set; }
    }
}
