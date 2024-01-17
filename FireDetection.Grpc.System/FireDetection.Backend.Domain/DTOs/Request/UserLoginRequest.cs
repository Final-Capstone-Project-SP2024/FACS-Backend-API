using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UserLoginRequest
    {
        public string? SecurityCode { get; set; }
        public string? Password { get; set; }
    }
}
