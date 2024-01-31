using AutoMapper;
using Firebase.Auth;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class CameraService : ICameraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITimerService _timerService;
        private readonly IMediaRecordService _mediaRecordService;

        public CameraService(IUnitOfWork unitOfWork, IMapper mapper, IMediaRecordService mediaRecordService, ITimerService timerService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediaRecordService = mediaRecordService;
            _timerService = timerService;
        }
        public async Task<CameInformationResponse> Active(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new Exception();

            camera.Status = "Active";
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameInformationResponse>(camera);
        }

        public async Task<CameInformationResponse> Add(AddCameraRequest request)
        {
            if (!await CheckDuplicateDestination(request.Destination)) throw new Exception();
            _unitOfWork.CameraRepository.InsertAsync(_mapper.Map<Camera>(request));
            await _unitOfWork.SaveChangeAsync();


            return _mapper.Map<CameInformationResponse>(await GetCameraByName(request.Destination));
        }

        public async Task<IQueryable<CameInformationResponse>> Get()
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            return (IQueryable<CameInformationResponse>)cameras;
        }

        public async Task<CameInformationResponse> Inactive(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new Exception();

            camera.Status = "Banned";
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameInformationResponse>(camera);

        }

        public async Task<CameInformationResponse> Update(Guid id, AddCameraRequest request)
        {
            Camera camera = await GetCameraById(id);
            camera.CameraDestination = request.Destination;
            camera.Status = request.Status;
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameInformationResponse>(_unitOfWork.CameraRepository.Where(x => x.Id == id));

        }

        public async Task<Camera> GetCameraByName(string cameraName)
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            return cameras.FirstOrDefault(x => x.CameraDestination == cameraName);
        }


        public async Task<Camera> GetCameraById(Guid cameraId)
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            return cameras.FirstOrDefault(x => x.Id == cameraId);
        }


        private async Task<bool> CheckDuplicateDestination(string CameraLocation)
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            var destination = cameras.FirstOrDefault(x => x.CameraDestination == CameraLocation);
            if (destination != null)
            {
                return false;
            }
            return true;
        }

        public async Task<DetectFireResponse> DetectFire(Guid id, TakeAlarmRequest request)
        {
            //1.get the people who have responsibility in this camreId,
            List<Guid> userIds = await _unitOfWork.CameraRepository.GetUsersByCameraId(id);
           

         /*   foreach (var userId in userIds)
            {
                //2. get fcmtoken from their userid
                await RealtimeDatabaseHandlers.GetFCMTokenByUserID(userId);
                // 3. push notification to their with messaging setting
                await CloudMessagingHandlers.CloudMessaging();

            }*/
            

            // 5. save record to database
            Record record = _mapper.Map<Record>(request);
            record.CameraID = id;
            record.Id = new Guid();

            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();
            // 4. save image and video to database 
            await _mediaRecordService.AddImage(request.PictureUrl, record.Id);
            await _mediaRecordService.Addvideo(request.VideoUrl, record.Id);
             _timerService.CheckIsVoting(record.Id);
            return _mapper.Map<DetectFireResponse>(record);
        }

        public async Task<DetectElectricalIncidentResponse> DetectElectricalIncident(Guid id, TakeElectricalIncidentRequest request)
        {

            await RealtimeDatabaseHandlers.GetFCMTokenByUserID();
            // 3. push notification to their with messaging setting
            await CloudMessagingHandlers.CloudMessaging();
            Record record = _mapper.Map<Record>(request);
            record.CameraID = id;
            record.Id = new Guid();
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<DetectElectricalIncidentResponse>(record);
        }


        private async Task SaveRecord(Record record)
        {
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
