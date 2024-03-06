using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class User : BaseEntity, IBaseCreated, IBaseModified
    {
        public string SecurityCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;  
        public string Status { get; set; } = null!;
        public bool isActive { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public Guid ModifiedBy { get; set; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<ControlCamera> ControlCameras { get; set; } 
        public ICollection<RecordProcess> RecordProcesses { get; set; }
   
    }
}
