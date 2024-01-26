using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddStaffRequest
    {
        public ICollection<Guid> staff {  get; set; }
    }

   
}
