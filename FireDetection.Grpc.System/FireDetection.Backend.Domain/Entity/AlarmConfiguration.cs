using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class AlarmConfiguration
    {
        public int AlarmConfigurationId {  get; set; }
        
        public decimal Start { get; set; }

        public decimal End { get; set; } 

        public int? NumberOfNotification { get; set; }
        public string AlarmNameConfiguration { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
