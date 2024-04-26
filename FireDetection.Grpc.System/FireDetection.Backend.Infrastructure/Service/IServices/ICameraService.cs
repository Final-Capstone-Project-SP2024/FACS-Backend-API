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

        Task<CameraInformationResponse> Update(Guid id ,UpdateCameraRequest request);

        Task<CameraInformationResponse> Active(Guid id);

        Task<CameraInformationResponse> Inactive(Guid id);


        Task<IQueryable<CameraInformationResponse>> Get();

        Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request,int alarmType);


       Task<DetectResponse> DetectElectricalIncident(Guid id);

        Task<CameraInformationResponse> FixCamera(Guid cameraId);

        public Task<Camera> GetCameraByName(string cameraName);

        public Task<CameraInformationDetailResponse> GetCameraDetail(Guid cameraId);


        public Task EnableReconnect();

        public Task<bool> CheckIsEnable();

        public Task<bool> ReconnectCamera(Guid cameraId);
    }
}
