using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class RecordProcessResponse
    {
        public Guid UserId { get; set; }
        public int VoteLevel { get; set; }
        public ActionProcessResponse VoteType { get; set; } = null!;
    }
}
