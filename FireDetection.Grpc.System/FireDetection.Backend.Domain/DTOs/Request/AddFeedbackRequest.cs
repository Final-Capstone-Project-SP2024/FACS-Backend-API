using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddFeedbackRequest
    {
        [MaxLength(100)]
        public string Context { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}
