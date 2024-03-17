using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Feedback : BaseEntity, IBaseCreated
    {
        public string Context { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get ; set ; }
        public Guid CreatedBy { get ; set; }


        public User User { get ; set; }
        public Guid UserId { get ; set; }
    }
}
