using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class ControlCamera : BaseEntity {
        public User User { get; set; }
        public Guid UserID { get; set; }

        public Camera Camera { get; set; }
        public Guid CameraID { get; set; }
    }
}
