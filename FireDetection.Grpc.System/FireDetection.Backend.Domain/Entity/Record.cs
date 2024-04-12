using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Record : BaseEntity, IBaseCreated
    {
        public DateTime RecordTime { get; set; }

        public string Status { get; set; } = null!;

        public decimal UserRatingPercent { get; set; } = 0;

        public decimal PredictedPercent { get; set; } = 0;


        public Camera Camera { get; set; }  

        public Guid CameraID { get; set; }

        public RecordType RecordType { get; set; } 

        public DateTime FinishAlarmTime { get; set; }
        public int RecordTypeID { get; set; }
        public ICollection<AlarmRate> AlarmRates { get; set; }
        public ICollection<MediaRecord> MediaRecords { get; set; }
        public ICollection<RecordProcess> RecordProcesses { get; set; }
        public ICollection<NotificationLog> NotificationLogs { get; set; }
        public DateTime CreatedDate { get ; set; }
        public Guid CreatedBy { get ; set ; }
    }
}
