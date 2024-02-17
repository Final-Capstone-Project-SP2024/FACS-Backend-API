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
    public interface ICameraService
    {
        Task<CameInformationResponse> Add(AddCameraRequest request);

        Task<CameInformationResponse> Update(Guid id ,AddCameraRequest request);

        Task<CameInformationResponse> Active(Guid id);

        Task<CameInformationResponse> Inactive(Guid id);


        Task<IQueryable<CameInformationResponse>> Get();

        Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request);


       Task<DetectResponse> DetectElectricalIncident(Guid id);



        public Task<Camera> GetCameraByName(string cameraName);
    }
}
