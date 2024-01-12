using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Record : BaseEntity
    {
        public string RecordTime { get; set; } = null!;

        public string Status { get; set; } = null!;

        public decimal UserRatingPercent { get; set; } = 0;

        public decimal PredictedPercent { get; set; } = 0;


        public Camera Camera { get; set; } = null!; 

        public Guid CameraID { get; set; }

        public ICollection<AlarmRate> AlarmRates { get; set; }
        public ICollection<MediaRecord> MediaRecords { get; set; }
        public ICollection<RecordProcess> RecordProcesses { get; set; }
     

    }
}
