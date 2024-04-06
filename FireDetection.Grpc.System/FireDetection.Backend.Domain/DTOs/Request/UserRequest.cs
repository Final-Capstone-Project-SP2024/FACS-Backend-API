using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UserRequest
    {
        public string? SecurityCode { get; set; } 
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public string? RoleName { get; set; }
        //public bool? isActive { get; set; }
        public string? Name { get; set; }
    }
}
