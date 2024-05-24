using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly FireDetectionDbContext _context;

        public LocationRepository(FireDetectionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<UserInLocation>> GetStaffInLocation(Guid locationId)
        {

            var result = _context.ControlCameras
                           .Include(x => x.User)
                           .Where(cc => cc.LocationID == locationId)
                           .Select(cc => new UserInLocation
                           {
                               UserId = cc.UserID,
                               UserName = cc.User.Name,
                           })
                           .ToList();

            return result;
        }

        public async Task<IEnumerable<LocationGeneralResponse>> GetLocations()
        {
            var result = _context.Locations.Include(x => x.ControlCameras).Include(x => x.Cameras).Select(x => new LocationGeneralResponse
            {
                LocationId = x.Id,
                LocationName = x.LocationName,
                NumberOfCamera = x.Cameras.Count,
                NumberOfSecurity = x.ControlCameras.Count(),
                LocationImage = x.LocationImage,
                IsDeleted = x.IsDeleted
            }).AsEnumerable();

            return result;
        }

        public async Task<IEnumerable<LocationGeneralResponse>> GetLocationsByUserRole(Guid userId)
        {
            var result = _context.Locations.Include(x => x.ControlCameras).Include(x => x.Cameras)
                .Where(x => x.ControlCameras.Select(x =>x.UserID).Contains(userId)) 
                .Select(x => new LocationGeneralResponse
                {
                    LocationId = x.Id,
                    LocationName = x.LocationName,
                    NumberOfCamera = x.Cameras.Count,
                    NumberOfSecurity = x.ControlCameras.Count(),
                    LocationImage = x.LocationImage,
                }).AsEnumerable();

            return result;
        }
    }
}
