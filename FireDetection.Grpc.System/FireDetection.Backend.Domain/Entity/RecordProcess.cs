using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class RecordProcess : BaseEntity, IBaseCreated
    {
        
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get ; set; }

        public User User { get; set; } = null!;

        public Guid UserID { get; set; }

        public Record Record { get; set; } = null!;

        public Guid RecordID { get; set; }  

        public ActionType ActionType { get; set; } = null!;

        public int ActionTypeId { get; set; }

    }
}
