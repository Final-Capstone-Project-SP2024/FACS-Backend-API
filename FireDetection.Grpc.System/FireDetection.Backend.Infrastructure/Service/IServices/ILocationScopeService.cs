using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ILocationScopeService
    {
        public Task<List<Guid>> GetUserInLocation(string LocationCode,int fireLevel);
        public Task<List<UserInLocationResponse>> GetUserLocation(Guid LocationId);
    }
}
