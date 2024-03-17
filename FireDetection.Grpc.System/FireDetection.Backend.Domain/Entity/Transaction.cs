using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public  class Transaction : BaseEntity, IBaseCreated, IBaseModified
    {
        public decimal Price { get; set; }
        public bool isPaid { get; set; }
        public User? User { get; set; }
        public Guid UserID { get; set; }
        public Contract? Contract { get; set; }
        public Guid ContractID { get; set; }
        public ActionPlanType? ActionPlanType { get; set; }
        public int ActionPlanTypeID { get; set; }
        public PaymentType? PaymentType { get; set; }
        public int PaymentTypeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }

     /*   public Transaction(decimal Price, bool IsPaid, Guid UserId, Guid ContractId, int PaymentTypeID)
        {
            this.Price = Price;
            isPaid = IsPaid;
            UserID = UserId;
            ContractID = ContractId;
            ActionPlanTypeID = ActionPlanTypeID;
            this.PaymentTypeID = PaymentTypeID;
        }*/
    }


  /*  public class RenewalTransaction : Transaction
    {
        public RenewalTransaction(decimal Price, bool IsPaid, Guid UserId, Guid ContractId, int PaymentTypeID) : base(Price, IsPaid, UserId, ContractId,  PaymentTypeID)
        {
            this.Price = Price;
            isPaid = IsPaid;
            UserID = UserId;
            ContractID = ContractId;
            ActionPlanTypeID = 3;
            this.PaymentTypeID = PaymentTypeID;
            CreatedDate = DateTime.UtcNow;
        }
    }



    public class UpgradeTransaction : Transaction
    {
        public UpgradeTransaction(decimal Price, bool IsPaid, Guid UserId, Guid ContractId, int PaymentTypeID) : base(Price, IsPaid, UserId, ContractId, PaymentTypeID)
        {
            this.Price = Price;
            isPaid = IsPaid;
            UserID = UserId;
            ContractID = ContractId;
            ActionPlanTypeID = 1;
            this.PaymentTypeID = PaymentTypeID;
            CreatedDate = DateTime.UtcNow;
        }
    }


    public class DowngradeTransaction : Transaction
    {
        public DowngradeTransaction(decimal Price, bool IsPaid, Guid UserId, Guid ContractId, int PaymentTypeID) : base(Price, IsPaid, UserId, ContractId, PaymentTypeID)
        {
            isPaid = IsPaid;
            UserID = UserId;
            ContractID = ContractId;
             ActionPlanTypeID = 2;
            this.PaymentTypeID = PaymentTypeID;
            CreatedDate = DateTime.UtcNow;
        }
    }*/
}
