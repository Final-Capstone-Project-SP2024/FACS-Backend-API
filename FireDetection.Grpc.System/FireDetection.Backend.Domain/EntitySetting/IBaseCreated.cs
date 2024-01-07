using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.EntitySetting
{
    public  interface IBaseCreated
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
