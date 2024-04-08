using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class UserInLocationResponse
    {
        public Guid UserID { get; set; }
        public string Name { get; set; } = null!;
    }
}
