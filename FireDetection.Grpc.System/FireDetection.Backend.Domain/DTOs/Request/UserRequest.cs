using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UserRequest
    {
        public string SecurityCode { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool isActive { get; set; }
        public string Name { get; set; } = null!;
    }
}
