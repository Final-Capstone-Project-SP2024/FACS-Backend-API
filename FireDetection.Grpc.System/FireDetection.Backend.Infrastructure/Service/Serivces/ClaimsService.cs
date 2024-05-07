using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            var id = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            GetCurrentUserId = Guid.TryParse(id, out Guid userId) ? GetCurrentUserId = userId : GetCurrentUserId = Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541");
            var role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
            GetCurrentUserRole = string.IsNullOrEmpty(role) ? string.Empty : role;
        }
        public Guid GetCurrentUserId { get; }
        public string GetCurrentUserRole { get; }
    }
}
