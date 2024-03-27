using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.State
{
    public static class RecordState
    {
        public const string InAlram = "InAlarm";
        

        public const string InVote = "InVote";

        public const string EndVote = "EndVote";

        public const string InAction = "InAction";
        
        public const string InFinish = "InFinish";

    }
}
