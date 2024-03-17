using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class FeedbackResponse
    {
        public Guid FeedbackId { get; set; }

        public string FeedbackContext { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
