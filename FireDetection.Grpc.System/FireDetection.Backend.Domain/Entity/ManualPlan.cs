using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class ManualPlan :  IBaseCreated, IBaseModified
    {
        [Key]
        public int ManualPlanId { get; set; }
        public string? ManualPlanName { get; set; } 

        public decimal Price { get; set; }

        public int LocationLimited { get; set; }

        public int CameraLimited { get; set; }

        public int UserLimited { get; set; }

        public DateTime CreatedDate { get ; set ; }
        public Guid CreatedBy { get; set ; }
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy {    get; set; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }

        //? connect with UserTransaction
        public ICollection<Contract> Contracts { get; set; }
    }
}
