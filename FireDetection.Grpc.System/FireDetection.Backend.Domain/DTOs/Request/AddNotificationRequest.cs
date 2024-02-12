using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddNotificationRequest
    {
        public string Title { get; set; }

        public string Context { get; set; }

        public string Header { get; set; }
    }
}
