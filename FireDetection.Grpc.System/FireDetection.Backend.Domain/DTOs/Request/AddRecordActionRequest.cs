using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddRecordActionRequest
    {
        public int ActionId { get; set; }

        [JsonIgnore]
        public Guid UserID { get; set; }

   
    }
}
