using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class NotificationLog : BaseEntity, IBaseCreated, IBaseModified
    {
        public int Count { get; set; }
        public DateTime LastModified { get; set; } 
        public Guid ModifiedBy { get ; set ; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        public NotificationType notificationType { get; set; }

        public int NotificationTypeId { get; set; }
        public Record Record { get; set; }

        public Guid RecordId { get; set; }
        public DateTime DeletedDate { get ; set ; }
    }
}
