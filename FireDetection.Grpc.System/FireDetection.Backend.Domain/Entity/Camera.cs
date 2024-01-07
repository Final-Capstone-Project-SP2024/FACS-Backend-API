using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Camera :BaseEntity
    {
        public string Status { get; set; } = null!;

        public string Location { get; set; } = null!;

        public ICollection<ControlCamera> ControlCameras { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}
