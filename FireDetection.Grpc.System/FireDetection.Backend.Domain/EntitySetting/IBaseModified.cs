using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.EntitySetting
{
    public interface IBaseModified
    {
        public DateTime LastModified { get; set; }

        public Guid ModifiedBy { get; set; }

        public Guid? DeleteBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
