using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        public Task<List<UserInLocation>> GetStaffInLocation(Guid locationId);

        public Task<IEnumerable<LocationGeneralResponse>> GetLocations();

        public Task<IEnumerable<LocationGeneralResponse>> GetLocationsByUserRole(Guid userId);
    }
}
