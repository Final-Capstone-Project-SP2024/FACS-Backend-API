using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public  interface ILocationService
    {
        Task<LocationInformationResponse> AddNewLocation(AddLocationRequest request);

        Task<LocationInformationResponse> RemoveSecurityInLocation(Guid locationId, AddStaffRequest request);
        Task<LocationInformationResponse> UpdateLocation(Guid locationId, AddLocationRequest request);
        Task<bool> DeleteLocation(Guid id);
        public  Task<IQueryable<LocationGeneralResponse>> GetLocation();
        Task<LocationInformationResponse> AddStaffToLocation(Guid locationId, AddStaffRequest request);
        Task<LocationInformationResponse> GetById(Guid locationId);

        

         
    }
}
