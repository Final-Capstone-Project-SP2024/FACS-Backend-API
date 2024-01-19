using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _context;

        public LocationService(IUnitOfWork context)
        {
            _context = context;
        }
        public async Task<LocationInformationResponse> AddNewLocation(AddLocationRequest request)
        {
            Location location = new Location()
            {
                LocationName = request.LocationName,
                CreatedDate = DateTime.UtcNow,

            };
            _context.LocationRepository.InsertAsync(location);
            await _context.SaveChangeAsync();


            Location locationTake = await GetLocationByName(request.LocationName);

            return new LocationInformationResponse()
            {
                CreatedDate = locationTake.CreatedDate,
                LocationName = request.LocationName,
            };
        }

        public async Task<bool> DeleteLocation(Guid id)
        {
            Location location = await GetLocationByID(id);
            location.IsDeleted = true;
            await _context.SaveChangeAsync();
            return true;
        }

        public async Task<IQueryable<Location>> GetLocation()
        {
            return await _context.LocationRepository.GetAll();
        }

        public async Task<LocationInformationResponse> UpdateLocation(Guid locationId, AddLocationRequest request)
        {
            Location location = await GetLocationByID(locationId);
            location.LocationName = request.LocationName;
            location.LastModified = DateTime.UtcNow;

            _context.LocationRepository.Update(location);
            await _context.SaveChangeAsync();

            Location testLocation = await _context.LocationRepository.GetById(locationId);
            return new LocationInformationResponse()
            {
                LocationName = testLocation.LocationName, 
                CreatedDate = testLocation.CreatedDate,
            };
        }

        private async Task<Location> GetLocationByName(string name)
        {
            IQueryable<Location> locations = await _context.LocationRepository.GetAll();

            return locations.FirstOrDefault(x => x.LocationName == name);

        }

        private async Task<Location> GetLocationByID(Guid id)
        {
            IQueryable<Location> locations = await _context.LocationRepository.GetAll();

            return locations.FirstOrDefault(x => x.Id == id);
        }
    }
}
