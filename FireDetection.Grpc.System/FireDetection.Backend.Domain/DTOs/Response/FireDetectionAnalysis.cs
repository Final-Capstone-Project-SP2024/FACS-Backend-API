using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class FireDetectionAnalysis
    {
        public string HighRiskFireArea { get; set; } = "Destination A";

        public int CountFireAlarmNotification { get; set; } = 0;

        public int CountFireAlarmLevel1 { get; set; } = 0;

        public int CountFireAlarmLevel2 { get; set; } = 0;

        public int CountFireAlarmLevel3 { get; set; } = 0;

        public int CountFireAlarmLevel4 { get; set; } = 0;

        public int CountFireAlarmLevel5 { get; set; } = 0;

        public int CountFakeAlarm { get; set; } = 0;
    }
}
