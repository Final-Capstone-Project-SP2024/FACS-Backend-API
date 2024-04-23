using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class AddLocationRequest
    {
        [Required]
        [RegularExpression(@"^Location [A-Z]$",ErrorMessage = "Must be Location .....")]
        public string LocationName { get; set; } = null!;
        public IFormFile LocationImage { get; set; }

        
    }
}
