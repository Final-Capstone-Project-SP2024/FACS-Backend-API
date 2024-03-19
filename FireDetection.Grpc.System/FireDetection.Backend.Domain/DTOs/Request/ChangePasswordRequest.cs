using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class ChangePasswordRequest
    {
        public string SecurityCode { get; set; }
        public int OTPSending { get; set; }

        public string newPassword { get; set; }
    }
}
