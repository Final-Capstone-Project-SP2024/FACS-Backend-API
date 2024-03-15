using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class CreateUserRequest
    {

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",ErrorMessage = "Wrong format gmail")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Wrong format phone")]
        public string Phone { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        [JsonIgnore]
        public int UserRole { get; set; } 
    }
}
