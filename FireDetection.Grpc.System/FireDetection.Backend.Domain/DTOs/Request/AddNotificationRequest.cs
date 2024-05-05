using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddNotificationRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Context { get; set; }

        [Required]
        public string Header { get; set; }
    }
}
