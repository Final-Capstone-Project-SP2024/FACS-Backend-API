using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddAlertByHandResponse
    {
        
        public int FireDetection { get; set; }
        [JsonIgnore]
        public string ImageDefault { get; set; } = "detectFireByUserImage.png";

        [JsonIgnore]    
        public string VideoDefault { get; set; } = "detectFireByUserVideo.mp4";
    }
}
