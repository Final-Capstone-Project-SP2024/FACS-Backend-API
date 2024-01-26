using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class VoteAlarmResponse
    {
        public Guid UserId { get; set; }

        public int LevelId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid RecordId { get; set; }

    }
}
