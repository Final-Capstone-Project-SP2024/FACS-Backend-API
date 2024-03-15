using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class BugsReport : BaseEntity, IBaseCreated, IBaseModified
    {
        public string Feature { get; set; }
        public string BugDescription { get; set; }
        public User User { get; set; }
        public Guid UserID { get; set; }
        public DateTime LastModified { get ; set ; }
        public Guid ModifiedBy { get; set ; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
