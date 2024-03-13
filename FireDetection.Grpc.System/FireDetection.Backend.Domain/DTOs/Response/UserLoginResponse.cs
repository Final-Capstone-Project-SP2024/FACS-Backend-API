using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.DTOs.Response
{
    public class UserLoginResponse
    {
        public Guid Id { get; set; }
        public string? SecurityCode { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public RoleResponse? Role { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
