using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IClaimsService
    {
        public Guid GetCurrentUserId { get; }
        public string GetCurrentUserRole { get; }
    }
}
