using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Transaction : BaseEntity, IBaseCreated, IBaseModified
    {
        public  decimal Price { get; set; }

        public  bool isPaid { get; set; }

        public User User { get; set; }

        public Guid UserID { get; set; }


        public UserTransaction UserTransaction { get; set; }

        public Guid UserTransactionID { get; set; }


        public ActionPlanType UserPlanType { get; set; }

        public int UserPlanTypeID { get; set; }

        public PaymentType PaymentType { get; set; }

        public int PaymentTypeID { get; set; }


        public DateTime CreatedDate { get ; set; }
        public Guid CreatedBy { get; set ; }
        public DateTime LastModified { get ; set ; }
        public Guid ModifiedBy { get; set   ; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
