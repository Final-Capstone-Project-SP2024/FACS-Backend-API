using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class ActionConfigurationResponse
    {
        public int ActionConfigurationId { get; set; } 
        public string? ActionConfigurationName { get; set; }

        public string? ActionConfigurationDescription { get; set;}

        public decimal Min { get; set; }

        public decimal Max { get; set; }    

            
    }
}
