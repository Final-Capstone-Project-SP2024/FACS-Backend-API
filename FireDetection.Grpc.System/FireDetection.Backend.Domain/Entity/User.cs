using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class User  : BaseEntity
    {
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;  

        public string Status { get; set; } = null!;
        public bool isActive { get; set; }

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; } = null!;

        public int RoleId { get; set; }

        public ICollection<ControlCamera> ControlCameras { get; set; } 
    }
}
