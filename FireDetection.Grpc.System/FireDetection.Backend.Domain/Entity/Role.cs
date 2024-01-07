using FireDetection.Backend.Domain.EntitySetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; } = null!;

        public ICollection<User> Users { get; set; }
    }
}
