using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Camera : BaseEntity, IBaseCreated, IBaseModified
    {
        public string Status { get; set; } = null!;
        public string CameraName { get; set; } = null!;
        public string CameraDestination { get; set; } = null!;
        public string CameraImage { get; set; } = null!;
        public Location Location { get; set; } = null!;
        public Guid LocationID { get; set; }
        public ICollection<Record> Records { get; set; } = null!;
        public DateTime LastModified { get; set ; }
        public Guid ModifiedBy { get ; set; }
        public Guid? DeleteBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
