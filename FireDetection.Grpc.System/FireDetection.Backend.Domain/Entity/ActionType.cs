using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class ActionType
    {
       public int ID { get; set; }

        public string ActionName { get; set; } = null!;
        public decimal Min { get; set; } = 0;
        public decimal Max { get; set; } = 0;

        public string ActionDescription { get; set; } = null!;

        public ICollection<RecordProcess>? RecordProcesses { get; set; }
    }
}
