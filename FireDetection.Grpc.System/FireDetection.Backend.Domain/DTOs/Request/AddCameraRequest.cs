using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddCameraRequest
    {

        [RegularExpression(@"(Active|Inactive)$", ErrorMessage = "Active or Inactive ")]
        public string Status { get; set; } = null!;
        [JsonIgnore]
        public string CameraName { get; set; } = "allow";
        public string Destination { get; set; } = null!;
        public Guid LocationId { get; set; }
    }
}
