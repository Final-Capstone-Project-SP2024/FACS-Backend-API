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
        Task<CameraInformationResponse> Add(AddCameraRequest request);

        Task<CameraInformationResponse> Update(Guid id ,AddCameraRequest request);

        Task<CameraInformationResponse> Active(Guid id);

        Task<CameraInformationResponse> Inactive(Guid id);


        Task<IQueryable<CameraInformationResponse>> Get();

        Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request);


       Task<DetectResponse> DetectElectricalIncident(Guid id);



        public Task<Camera> GetCameraByName(string cameraName);
    }
}
