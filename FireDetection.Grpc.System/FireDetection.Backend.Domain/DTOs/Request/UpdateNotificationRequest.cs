using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class UpdateNotificationRequest
    {
        [Required]
        [RegularExpression(@"^(?=.*\blocation\b).*$", ErrorMessage = "Must contain the word 'Location'")]
        public string Title { get; set; } = null!;
      
        [Required]
        [RegularExpression(@"^(?=.*\blocation\b)(?=.*\bdestination\b).*$", ErrorMessage = "Must contain both 'Location' and 'Destination'")]
        public string Context { get; set; } = null!;

    }
}
