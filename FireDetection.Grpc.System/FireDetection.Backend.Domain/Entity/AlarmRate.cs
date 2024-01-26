using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class AlarmRate : BaseEntity, IBaseCreated
    {
        public DateTime Time { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }

        public User User { get; set; } = null!;
        public Guid UserID { get; set; }
        public Record Record { get; set; } = null!;

        public Guid RecordID { get; set; }

        public Level Level { get; set; } = null!;

        public int LevelID { get; set; }
    }
}
