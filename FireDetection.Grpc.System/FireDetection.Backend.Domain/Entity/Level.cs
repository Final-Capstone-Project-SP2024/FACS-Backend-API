using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Level
    {
        public int LevelID { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
        public ICollection<AlarmRate> AlarmRates { get; set; }  

    }
}
