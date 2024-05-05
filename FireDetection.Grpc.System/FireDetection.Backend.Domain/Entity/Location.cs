using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Location : BaseEntity, IBaseModified, IBaseCreated
    {
        public string LocationName { get; set; } 
        public string LocationImage { get; set; }
        public DateTime LastModified { get ; set ; }
        public Guid ModifiedBy { get; set ; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public virtual ICollection<Camera> Cameras { get; set; }    
        public  virtual ICollection<ControlCamera> ControlCameras { get; set; }
        public DateTime DeletedDate { get ; set ; }
    }
}
