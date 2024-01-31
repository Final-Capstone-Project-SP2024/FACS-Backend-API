using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Repository.IRepositories;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LocationInformationResponse> AddNewLocation(AddLocationRequest request)
        {

            if (!await checkDuplicateLocationName(request.LocationName))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");
            };

            _context.LocationRepository.InsertAsync(_mapper.Map<Location>(request));
            await _context.SaveChangeAsync();

            return _mapper.Map<LocationInformationResponse>(await GetLocationByName(request.LocationName));
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
            if(location is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, " Not found this locations");
            location.LocationName = request.LocationName;
            location.LastModified = DateTime.UtcNow;

            _context.LocationRepository.Update(location);
            await _context.SaveChangeAsync();

            return _mapper.Map<LocationInformationResponse>(await _context.LocationRepository.GetById(locationId));
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


        private async Task<bool> checkDuplicateLocationName(string locationName)
        {
            IQueryable<Location> locations = await _context.LocationRepository.GetAll();
            if (locations.FirstOrDefault(x => x.LocationName == locationName) is null) return true;

            return false;
        }

        public async Task<LocationInformationResponse> AddStaffToLocation(Guid locationId, AddStaffRequest request)
        {
            foreach (var staff in request.staff)
            {

                ControlCamera controlCamera = new ControlCamera()
                {
                    LocationID = locationId,
                    UserID = staff  
                };
                _context.ControlCameraRepository.InsertAsync(controlCamera);
                await _context.SaveChangeAsync();

            }

            var data = await _context.LocationRepository.GetStaffInLocation(locationId);
            return new LocationInformationResponse()
            {
                LocationId = locationId,
                Users = data
            };
        }


        private async Task<bool> CheckDuplicateUserInControlCamera(Guid locationId, Guid userId)
        {
            var check = _context.ControlCameraRepository.Where(x => x.LocationID == locationId && x.UserID == userId);
            if (check is not null)
            {
                return false;
            }

            return true;
        }
    }
}
