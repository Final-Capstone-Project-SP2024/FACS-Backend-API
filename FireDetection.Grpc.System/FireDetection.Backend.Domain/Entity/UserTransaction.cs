using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class UserTransaction : BaseEntity, IBaseModified, IBaseCreated
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool isPaid { get; set; }

        public string ContractImage { get; set; }

        //? connect to manual plan 
        public ManualPlan ManualPlan { get; set; }

        public int ManualPlanID { get; set; }
        //? connect to user
        public User User { get; set; }
        //? connect to transaction
        public ICollection<Transaction> Transaction { get; set; }

        public Guid UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set ; }
        public Guid CreatedBy { get ; set ; }
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
