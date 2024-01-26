using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Request
{
    public class CreateUserRequest
    {

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^0[0-9]{9}$")]
        public string Phone { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int UserRole { get; set; } 
    }
}
