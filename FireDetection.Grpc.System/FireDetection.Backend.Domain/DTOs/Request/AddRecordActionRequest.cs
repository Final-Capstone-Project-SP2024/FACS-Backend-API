using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddRecordActionRequest
    {
        public int ActionId { get; set; }

        public Guid UserID { get; set; }

   
    }
}
