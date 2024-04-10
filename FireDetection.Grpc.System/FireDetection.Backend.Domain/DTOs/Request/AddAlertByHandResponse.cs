using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddAlertByHandResponse
    {
        public IFormFile video { get ; set; }

        public IFormFile image { get ; set; }   

        public int FireDetection { get; set; }

    }
}
