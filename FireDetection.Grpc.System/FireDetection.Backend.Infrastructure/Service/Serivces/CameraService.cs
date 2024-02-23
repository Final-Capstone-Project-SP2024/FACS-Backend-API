using AutoMapper;
using Firebase.Auth;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Utils;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
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
        private readonly IMemoryCacheService _memoryCacheService;

        public CameraService(IUnitOfWork unitOfWork, IMapper mapper, IMediaRecordService mediaRecordService, ITimerService timerService, IMemoryCacheService memoryCacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediaRecordService = mediaRecordService;
            _timerService = timerService;
            _memoryCacheService = memoryCacheService;
        }
        public async Task<CameraInformationResponse> Active(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new Exception();

            camera.Status = "Active";
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameraInformationResponse>(camera);
        }

        public async Task<CameraInformationResponse> Add(AddCameraRequest request)
        {
            if (!await CheckDuplicateDestination(request.Destination)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Destination have already add in system");

            _unitOfWork.CameraRepository.InsertAsync(_mapper.Map<Camera>(request));
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameraInformationResponse>(await GetCameraByName(request.Destination));
        }

        public async Task<IQueryable<CameraInformationResponse>> Get()
        {
            return await _unitOfWork.CameraRepository.GetAllViewModel();   
        }

        public async Task<CameraInformationResponse> Inactive(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new Exception();

            camera.Status = "Banned";
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameraInformationResponse>(camera);

        }

        public async Task<CameraInformationResponse> Update(Guid id, AddCameraRequest request)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            camera.CameraDestination = request.Destination;
            camera.Status = request.Status;
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();
            return _mapper.Map<CameraInformationResponse>(await _unitOfWork.CameraRepository.GetById(id));

        }

        public async Task<Camera> GetCameraByName(string cameraName)
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            return cameras.FirstOrDefault(x => x.CameraDestination == cameraName);
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

        public async Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");

            //1.get the people who have responsibility in this camreId,
            List<Guid> userIds = await _unitOfWork.CameraRepository.GetUsersByCameraId(id);


            /*   foreach (var userId in userIds)
               {
                   //2. get fcmtoken from their userid
                   await RealtimeDatabaseHandlers.GetFCMTokenByUserID(userId);
                   // 3. push notification to their with messaging settinga

               }*/
            //? Send notification about where have the fire belong to where location
            NotficationDetailResponse data = await NotificationHandler.Get(8);

            await CloudMessagingHandlers.CloudMessaging(
                HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination),
                HandleTextUtil.HandleContext(
                    data.Context,
                    _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName,
                    camera.CameraDestination)
                );


            // 5. save record to database
            Record record = _mapper.Map<Record>(request);
            record.CameraID = id;
            record.Id = Guid.NewGuid();
            await _memoryCacheService.CreateRecordKey(record.Id);

            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();

            // 4. save image and video to database 
            await _mediaRecordService.AddImage(request.PictureUrl, record.Id);
            await _mediaRecordService.Addvideo(request.VideoUrl, record.Id);


            _timerService.CheckIsVoting(record.Id);
            return _mapper.Map<DetectResponse>(record);
        }

        public async Task<DetectResponse> DetectElectricalIncident(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");

            //? Send notification about where have the fire belong to where location
            NotficationDetailResponse data = await NotificationHandler.Get(4);

            // 3. push notification to their with messaging setting
            await CloudMessagingHandlers.CloudMessaging(
               HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination),
               HandleTextUtil.HandleContext(
                   data.Context,
                   _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName,
                   camera.CameraDestination)
               );
            Record record = new();
            record.CameraID = id;
            record.Id = new Guid();
            record.Status = RecordState.InAlram;
            record.RecordTypeID = 2;
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<DetectResponse>(record);
        }


        private async Task SaveRecord(Record record)
        {
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
