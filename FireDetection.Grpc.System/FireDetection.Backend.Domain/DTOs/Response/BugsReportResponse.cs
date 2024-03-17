using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public  class BugsReportResponse
    {
        public Guid BugId { get; set; }
        public string Feature { get; set; }
        public string BugDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSolved { get; set; }

    }
}
