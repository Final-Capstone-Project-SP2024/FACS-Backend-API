using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class ControlCamera : BaseEntity {
        public User User { get; set; }  = new User();
        public Guid UserID { get; set; }

        public Location Location { get; set; } = new Location();
        public Guid LocationID { get; set; }
    }
}
