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
using Google.Api.Gax.ResourceNames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class CameraService : ICameraService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITimerService _timerService;
        private readonly IMediaRecordService _mediaRecordService;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILocationScopeService _locationScopeService;
        private readonly IClaimsService _claimService;
        private static Timer timer;
        private static bool apiEnabled = false;

        public CameraService(IUnitOfWork unitOfWork, IMapper mapper, IMediaRecordService mediaRecordService, ITimerService timerService, IMemoryCacheService memoryCacheService, ILocationScopeService locationScopeService, IClaimsService claims)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediaRecordService = mediaRecordService;
            _timerService = timerService;
            _memoryCacheService = memoryCacheService;
            _locationScopeService = locationScopeService;
            _claimService = claims;

        }
        public async Task<CameraInformationResponse> Active(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "This CameraId not  in system");

            camera.Status = CameraType.Connect;
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

            Camera camera = _mapper.Map<Camera>(request);
            camera.CameraImage = request.CameraImage.FileName.ToString();

            _unitOfWork.CameraRepository.InsertAsync(camera);

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

            camera.Status = CameraType.Disconnect;
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

        public async Task<CameraInformationResponse> Update(Guid id, UpdateCameraRequest request)
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

        private async Task<bool> CheckCameraIsConnected(Guid id)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if(camera.Status == CameraType.Disconnect)
            {
                return false;
            }
            return true;

        } 

        public async Task<DetectResponse> DetectFire(Guid id, TakeAlarmRequest request, int alarmType)
        {

            if (await _unitOfWork.CameraRepository.GetById(id) is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");
            //? check is Disconnected 
            if(await CheckCameraIsConnected(id) is false)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Camera is disconnected");
            }
            int DangerRecord = await IsDangerRecord(request.PredictedPercent);
            //TODO: Check camera in system 
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            string locationName = _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName;


            //TODO: save record to database
            Record record = _mapper.Map<Record>(request);
            record.RecommendAlarmLevel = recommentActionAlarm(request.PredictedPercent);
            record.AlarmConfigurationId = DangerRecord;
            record.CameraID = id;
            record.Id = Guid.NewGuid();
            if (alarmType == 3)
            {
                record.CreatedBy = _claimService.GetCurrentUserId;
                record.RecordTypeID = 3;
            }

            //? add tp check 
            await _memoryCacheService.Create(record.Id, CacheType.FireNotify);
            await _memoryCacheService.Create(record.Id, CacheType.IsVoting);

            if (DangerRecord != 1)
            {
                List<Guid> users = await _locationScopeService.GetUserInLocation(locationName, 1);
                users.Add(Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"));
                //todo  create checking user do the next task ( vote task) and sending notification 
                _timerService.CheckIsVoting(record.Id, camera.CameraDestination, locationName, users);

            }
            else
            {
                List<Guid> users = await _locationScopeService.GetUserInLocation(locationName, 1);
                users.Add(Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"));
                NotficationDetailResponse data = await NotificationHandler.Get(11);
                //? notification one time
              
                    foreach (var item in users)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine(HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination));
                        Console.WriteLine(HandleTextUtil.HandleContext(data.Context, locationName, camera.CameraDestination));
                        string token = await RealtimeDatabaseHandlers.GetFCMTokenByUserID(item);
                        string tokenReduce = token.Replace("\"", "");
                        await CloudMessagingHandlers.CloudMessaging(
                                   HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination),
                                   HandleTextUtil.HandleContext(data.Context, locationName, camera.CameraDestination), tokenReduce
                                  );
                    }
            }


            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();

            // 4. save image and video to database 
            await _mediaRecordService.AddImage(request.PictureUrl.ToString(), record.Id);
            await _mediaRecordService.Addvideo(request.VideoUrl.ToString(), record.Id);


            return _mapper.Map<DetectResponse>(record);
        }
        protected async Task<int> IsDangerRecord(decimal predictedAI)
        {
            AlarmConfiguration alarm = await _unitOfWork.AlarmConfigurationRepository.GetAlarmConfigurationDetail(1);
            if (predictedAI >= alarm.Start && predictedAI < alarm.End)
            {
                return 1;
            }
            return 2;
        }
        public async Task<DetectResponse> DetectElectricalIncident(Guid id)
        {

            //todo check cameraID in system 
            Camera camera = await _unitOfWork.CameraRepository.GetById(id);
            if (camera is null) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "CameraId is invalid");
            Location location = await _unitOfWork.LocationRepository.GetById(camera.LocationID);


            //todo Spam disconnect action
            //? after 1 minutes end the action return to finish
            _timerService.DisconnectionNotification(id, camera.CameraDestination, location.LocationName);
            //todo Send notification about where have the fire belong to where location
            /*     NotficationDetailResponse data = await NotificationHandler.Get(7);
                 await CloudMessagingHandlers.CloudMessaging(
                    HandleTextUtil.HandleTitle(data.Title, camera.CameraDestination),
                    HandleTextUtil.HandleContext(
                        data.Context,
                        _unitOfWork.LocationRepository.GetById(camera.LocationID).Result.LocationName,
                        camera.CameraDestination)
                    );*/

            camera.Status = CameraType.Disconnect;
            _unitOfWork.CameraRepository.Update(camera);

            //todo save record to the database 
            Record record = new();
            record.CameraID = id;
            record.Id = new Guid();
            record.Status = RecordState.InAlram;
            record.RecordTypeID = 2;
            record.AlarmConfigurationId = 1;
            record.RecommendAlarmLevel = "No Recommend";
            record.RecordTime = DateTime.UtcNow.AddHours(7);
            record.CreatedDate = DateTime.UtcNow.AddHours(7);
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<DetectResponse>(record);
        }
        private static string recommentActionAlarm(decimal predictedNumber) => predictedNumber switch
        {
            var num when (num >= 0 && num < 20) => "Alarm Level 1",
            var num when (num >= 20 && num < 40) => "Alarm Level 2",
            var num when (num >= 40 && num < 60) => "Alarm Level 3",
            var num when (num >= 60 && num < 80) => "Alarm Level 4",
            var num when (num >= 80 && num < 100) => "Alarm Level 5",
        };


        private async Task SaveRecord(Record record)
        {
            _unitOfWork.RecordRepository.InsertAsync(record);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<CameraInformationResponse> FixCamera(Guid cameraId)
        {
            Camera camera = await _unitOfWork.CameraRepository.GetById(cameraId);
            camera.Status = CameraType.Connect;
            await _unitOfWork.SaveChangeAsync();
            await SetFinishRecord(cameraId);
            return _mapper.Map<CameraInformationResponse>(camera);
        }

        public async Task<CameraInformationDetailResponse> GetCameraDetail(Guid cameraId)
        {
            
            Camera camera =  await _unitOfWork.CameraRepository.GetById(cameraId);
            if(camera is null)
            {
                throw new Exception();
            }
            return new CameraInformationDetailResponse
            {
                CameraDestination = camera.CameraDestination,
                CameraId = cameraId,
                CameraImage = camera.CameraImage,
                CameraName = camera.CameraName,
                RecordCount = _unitOfWork.RecordRepository.Where(x => x.CameraID == cameraId).Count(),
                Status = camera.Status
            };

        }

        public async Task EnableReconnect()
        {
            apiEnabled = true;
            timer = new Timer(DisableAPI, null, 60000, Timeout.Infinite);
            ///900000
        }


        protected static void DisableAPI(object state)
        {
            apiEnabled = false;
            Console.WriteLine("API access disabled.");
        }
        public async Task<bool> CheckIsEnable()
        {
            return apiEnabled;

        }

        public async Task<bool> ReconnectCamera(Guid cameraId)
        {
            
            //? 0
            var camera = await _unitOfWork.CameraRepository.GetById(cameraId);
            if(camera.Status == CameraType.Disconnect)
            {
                DisableAPI(camera);
                await SetFinishRecord(cameraId);
                camera.Status = CameraType.Connect;
                _unitOfWork.CameraRepository.Update(camera);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            
            //? +1
            var cameraNext = await _unitOfWork.CameraRepository.Where(x => x.CameraName == IncrementCameraName(camera.CameraName)).FirstOrDefaultAsync();
            if (cameraNext.Status == CameraType.Disconnect)
            {
                DisableAPI(camera);
                await SetFinishRecord(cameraId);
                cameraNext.Status = CameraType.Connect;
                _unitOfWork.CameraRepository.Update(camera);
                await _unitOfWork.SaveChangeAsync();

                return true;
            }

            //? -1 
            var cameraUnder = await _unitOfWork.CameraRepository.Where(x => x.CameraName == DecrementCameraName(camera.CameraName)).FirstOrDefaultAsync();
            if (cameraUnder.Status == CameraType.Disconnect)
            {
                DisableAPI(camera);
                await SetFinishRecord(cameraId);
                cameraUnder.Status = CameraType.Connect;
                _unitOfWork.CameraRepository.Update(camera);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }


            return false;

        }

        private async Task SetFinishRecord(Guid cameraId)
        {
            Record record =  await _unitOfWork.RecordRepository.Where(x => x.Status == RecordState.InAlram && x.CameraID == cameraId).FirstOrDefaultAsync();
            record.Status = RecordState.InFinish;
            record.FinishAlarmTime = DateTime.UtcNow.AddHours(7);
            _unitOfWork.RecordRepository.Update(record);
            await _unitOfWork.SaveChangeAsync();
        }


        private string IncrementCameraName(string cameraName)
        {
            // Extract the number from the camera name
            string prefix = cameraName.Substring(0, cameraName.Length - 3);
            int number = int.Parse(cameraName.Substring(cameraName.Length - 3)) + 1;
            return $"{prefix}{number:D3}";
        }


        private string DecrementCameraName(string cameraName)
        {
            // Extract the number from the camera name
            string prefix = cameraName.Substring(0, cameraName.Length - 3);
            int number = int.Parse(cameraName.Substring(cameraName.Length - 3)) - 1;
            return $"{prefix}{number:D3}";
        }
    }
}
