using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface ICameraService
    {
        Task<CameInformationResponse> Add(AddCameraRequest request);

        Task<CameInformationResponse> Update(Guid id ,AddCameraRequest request);

        Task<CameInformationResponse> Active(Guid id);

        Task<CameInformationResponse> Inactive(Guid id);


        Task<IQueryable<CameInformationResponse>> Get();

        Task<DetectFireResponse> DetectFire(Guid id, TakeAlarmRequest request);


        Task<DetectElectricalIncidentResponse> DetectElectricalIncident(Guid id, TakeElectricalIncidentRequest request);
    }
}
