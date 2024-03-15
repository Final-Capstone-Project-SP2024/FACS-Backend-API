using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class BugsReportRequest
    {
        public string Feature { get; set; }
        public string BugDescription { get; set; }

    }
}
