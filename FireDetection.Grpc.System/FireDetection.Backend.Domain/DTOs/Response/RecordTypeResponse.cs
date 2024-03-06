using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class RecordTypeResponse
    {
        public int RecordTypeId { get; set; }
        public string RecordTypeName { get; set; } = null!;
    }
}
