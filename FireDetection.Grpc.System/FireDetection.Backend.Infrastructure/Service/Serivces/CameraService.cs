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
using System.Linq.Dynamic.Core;
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
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "This CameraId not  in system");

            camera.Status = CameraType.Active;
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameraInformationResponse>(camera);
        }

        public async Task<CameraInformationResponse> Add(AddCameraRequest request)
        {
           
            request.CameraName = await GenerateCameraName();
            if (await _unitOfWork.LocationRepository.GetById(request.LocationId) is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "LocationId not in the system");

            if (!await CheckDuplicateDestination(request.Destination)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Destination have already add in system");

            if (!await CheckDuplicateCameraName(request.CameraName)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Camera Name have already add in system");



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
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Camera is not in system");

            camera.Status = CameraType.Ban;
            camera.LastModified = DateTime.UtcNow;

            _unitOfWork.CameraRepository.Update(camera);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<CameraInformationResponse>(camera);

        }
        internal async Task<string> GenerateCameraName()
        {
            var cameraNameQuery = _unitOfWork.CameraRepository.GetAll();
            if (cameraNameQuery.Result.Count() == 0)
            {
                return "camera_001";
            }
            else
            {
                int number = int.Parse(cameraNameQuery.Result.OrderByDescending(x => x.CreatedDate).FirstOrDefault().CameraName.Substring(7));
                number++;
                return $"camera_{number:D3}";

            }
        }

        public async Task<CameraInformationResponse> Update(Guid id, AddCameraRequest request)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId not in system");

            if (!await CheckDuplicateDestination(request.Destination)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Destination have already add in system");

            if (!await CheckDuplicateCameraName(request.CameraName)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Camera Name have already add in system");
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

        internal async Task<bool> CheckCameraIdInSystem(Guid cameraId)
        {
            var camera = await _unitOfWork.CameraRepository.GetById(cameraId);
            if (camera is null)
            {
                return false;
            }
            return true;
        }



        private async Task<bool> CheckDuplicateCameraName(string CameraName)
        {
            IQueryable<Camera> cameras = await _unitOfWork.CameraRepository.GetAll();
            var destination = cameras.FirstOrDefault(x => x.CameraName == CameraName);
            if (destination != null)
            {
                return false;
            }
            return true;
        }
        public async Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request)
        {

            //TODO: Check camera in system 
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            string locationName = _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName;

            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");



            //TODO: save record to database
            Record record = _mapper.Map<Record>(request);
            record.CameraID = id;
            record.Id = Guid.NewGuid();


            //? add tp check 
            await _memoryCacheService.Create(record.Id, CacheType.FireNotify);
            await _memoryCacheService.Create(record.Id, CacheType.IsVoting);
            //todo  create checking user do the next task ( vote task) and sending notification 
            _timerService.CheckIsVoting(record.Id, camera.CameraDestination, locationName);

            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();
            //! get the people who have responsibility in this camreId,
            // List<Guid> userIds = await _unitOfWork.CameraRepository.GetUsersByCameraId(id);

            // 4. save image and video to database 
            await _mediaRecordService.AddImage(request.PictureUrl.ToString(), record.Id);
            await _mediaRecordService.Addvideo(request.VideoUrl.ToString(), record.Id);


            return _mapper.Map<DetectResponse>(record);
        }

        public async Task<DetectResponse> DetectElectricalIncident(Guid id)
        {

            //todo check cameraID in system 
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");

            //todo Send notification about where have the fire belong to where location
            NotficationDetailResponse data = await NotificationHandler.Get(4);
            await CloudMessagingHandlers.CloudMessaging(
               HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination),
               HandleTextUtil.HandleContext(
                   data.Context,
                   _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName,
                   camera.CameraDestination)
               );


            //todo save record to the database 
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
