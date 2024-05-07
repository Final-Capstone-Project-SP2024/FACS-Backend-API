using AutoMapper;
using AutoMapper.QueryableExtensions;
using Firebase.Auth;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Helpers.GetHandler;
using FireDetection.Backend.Domain.Utils;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Helpers.FirebaseHandler;
using FireDetection.Backend.Infrastructure.Helpers.GetHandler;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
using Google.Api.Gax.ResourceNames;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class RecordService : IRecordService
    {
        private readonly ITimerService _timerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly INotificationLogService _log;
        private readonly IClaimsService _claimService;
        private readonly ILocationScopeService _locationScopeService;
        private readonly IUserResponsibilityService _userResponsibilityService;

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCacheService memoryCacheService, ITimerService timerService, INotificationLogService log, IClaimsService claimService, ILocationScopeService locationScopeService, IUserResponsibilityService userResponsibilityService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCacheService = memoryCacheService;
            _timerService = timerService;
            _log = log;
            _claimService = claimService;
            _locationScopeService = locationScopeService;
            _userResponsibilityService = userResponsibilityService;
        }
        public async Task<bool> ActionInAlarm(Guid recordID, AddRecordActionRequest request)
        {
            request.UserID = _claimService.GetCurrentUserId;
            await _memoryCacheService.UnCheck(recordID, CacheType.IsVoting);
            //? check input can action type 
            if (!await CheckActionInSystem(recordID, request.ActionId)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Can not action smaller than the action before");

            // if (!await checkActionInRecord(recordID)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");

            if (request.ActionId < 6)
            {
                //todo save data int variale sutiable with request.actionID
                await _memoryCacheService.Create(recordID, setKey(request.ActionId));
                await _memoryCacheService.Create(recordID, CacheType.Voting);
                await _memoryCacheService.Create(recordID, CacheType.IsFinish);
                string LocationName =  await _unitOfWork.RecordRepository.GetLocationName(recordID);
                List<Guid> users = await _locationScopeService.GetUserInLocation(LocationName, request.ActionId);
                users.Add(Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"));
                Console.WriteLine(users);
                foreach (var user in users)
                {
                   await  _userResponsibilityService.SaveUserInNotification(recordID, user);

                }
                string cameraDestination = _unitOfWork.CameraRepository.GetById(_unitOfWork.RecordRepository.GetById(recordID).Result.CameraID).Result.CameraDestination;
                _timerService.SpamNotification(recordID, request.ActionId, users, cameraDestination, LocationName);
            }


            //? cancel check
            await _memoryCacheService.UnCheck(recordID, CacheType.Action);
            if (request.ActionId == 6)
            {
                await _memoryCacheService.UnCheck(recordID, CacheType.IsFinish);
                List<RecordProcess> idInput = _unitOfWork.RecordProcessRepository.Where(x => x.RecordID == recordID).ToList();
                List<int> output = new List<int>();
                foreach (var item in idInput)
                {
                    output.Add(item.ActionTypeId);
                }
                //_log.SaveNotificationVotingRequire(recordID, await _memoryCacheService.GetResult(recordID, CacheType.VotingValue));
                foreach (var item in output)
                {
                    _log.SaveNotificationActionRequire(recordID, await _memoryCacheService.GetResult(recordID, setKey(item)), setKey(item));
                    Console.WriteLine(await _memoryCacheService.GetResult(recordID, setKey(item)));
                }
                await _unitOfWork.SaveChangeAsync();

                //? save who is finish
                RecordProcess recordProcessEnding = _mapper.Map<RecordProcess>(request);
                recordProcessEnding.RecordID = recordID;
                _unitOfWork.RecordProcessRepository.InsertAsync(recordProcessEnding);
                await _unitOfWork.SaveChangeAsync();
                await updateRecordToEnd(recordID);
                return true;
            }
            if (request.ActionId == 7)
            {
                await NotificationFakeAlarm(await _unitOfWork.RecordRepository.GetLocationName(recordID));
                await _memoryCacheService.UnCheck(recordID, CacheType.IsFinish);
                await updateRecordToEnd(recordID);
                RecordProcess recordProcessEnding = _mapper.Map<RecordProcess>(request);
                recordProcessEnding.RecordID = recordID;
                _unitOfWork.RecordProcessRepository.InsertAsync(recordProcessEnding);
                await _unitOfWork.SaveChangeAsync();


                // updateRecordToEnd(recordID, 1);

                return true;
            };

            await ChangeInActionRecordState(recordID);


            RecordProcess recordProcess = _mapper.Map<RecordProcess>(request);
            recordProcess.RecordID = recordID;
            _unitOfWork.RecordProcessRepository.InsertAsync(recordProcess);
            await _unitOfWork.SaveChangeAsync();


            // updateRecordToEnd(recordID, 1);

            return true;
        }

        internal async Task ChangeInActionRecordState(Guid recordId)
        {
            var record = await _unitOfWork.RecordRepository.GetById(recordId);
            //record.UserRatingPercent = await _memoryCacheService.GetResult(recordId, CacheType.VotingValue);
            record.Status = RecordState.InAction;
            _unitOfWork.RecordRepository.Update(record);
            await _unitOfWork.SaveChangeAsync();


        }

        private static CacheType setKey(int actionid) => actionid switch
        {
            1 => CacheType.AlarmLevel1,
            2 => CacheType.AlarmLevel2,
            3 => CacheType.AlarmLevel3,
            4 => CacheType.AlarmLevel4,
            5 => CacheType.AlarmLevel5,
        };


        public async Task<bool> VoteAlarmLevel(Guid recordID, RateAlarmRequest request)
        {
            throw new NotImplementedException();
            //var userId = _claimService.GetCurrentUserId;
            //if (_unitOfWork.AlarmRateRepository.Where(x => x.RecordID == recordID && x.UserID == userId).FirstOrDefault() is not null)
            //{
            //    throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "You have voted before");
            //}
            ////Create Voting Point for RecordId 
            //if (_unitOfWork.AlarmRepository.Where(x => x.RecordID == recordID).FirstOrDefault() is null)
            //{

            //    //? to get the biggest value : create
            //    //wait _memoryCacheService.Create(recordID, CacheType.VotingLevel);

            //    await _memoryCacheService.Create(recordID, CacheType.VotingValue, request.LevelRating);

            //    //? to count how many vote in this record 
            //    await _memoryCacheService.Create(recordID, CacheType.VotingCount, 1);


            //}
            //else
            //{
            //    //? to get the biggest value : set 
            //    await _memoryCacheService.SettingCount(recordID, CacheType.VotingLevel, request.LevelRating);


            //    //? to count how many vote in this record 
            //    await _memoryCacheService.IncreaseQuantity(recordID, CacheType.VotingCount);
            //}

            ////todo after 5 vote or 2 minutes it will auto user can not voting for this userId
            ////? set voteCount
            ////? start 2 minutes to end 
            ////_timerService.EndVotePhase(recordID);


            ////todo check and create action checking 
            //await _memoryCacheService.UnCheck(recordID, CacheType.IsVoting);
            //await _memoryCacheService.Create(recordID, CacheType.Action);
            //List<Guid> users = await _locationScopeService.GetUserInLocation(await _unitOfWork.RecordRepository.GetLocationName(recordID), 1);
            //users.Add(Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"));
            //_timerService.CheckIsAction(recordID, users);


            ////Console.WriteLine(await _memoryCacheService.GetResult(recordID, CacheType.FireNotify));

            ////? update to another in Record (InVote)
            //await RecordInVote(recordID);
            ////todo save log the fireNotify Task
            //await _log.SaveNotificationFireNotifyLog(recordID, await _memoryCacheService.GetResult(recordID, CacheType.FireNotify));
            //request.LevelRating = request.LevelRating == 0 ? 6 : request.LevelRating;
            //alarmRate.RecordID = recordID;
            //alarmRate.UserID = userId;
            //_unitOfWork.AlarmRateRepository.InsertAsync(alarmRate);
            //return await _unitOfWork.SaveChangeAsync() > 0;

        }


        internal async Task RecordInVote(Guid recordId)
        {
            var record = await _unitOfWork.RecordRepository.GetById(recordId);
            if (record.Status == RecordState.InAlram)
            {
                record.Status = RecordState.InVote;
                _unitOfWork.RecordRepository.Update(record);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<bool> CheckActionInSystem(Guid recordId, int levelid)
        {
            int actionTypeId = _unitOfWork.RecordProcessRepository.Where(x => x.RecordID == recordId).FirstOrDefault()?.ActionTypeId ?? 0;
            if (actionTypeId == 0)
            {
                return true;

            }
            else
            {
                if (actionTypeId < levelid)
                {
                    return true;
                }
            }

            return false;
        }


        private async Task<bool> checkActionInRecord(Guid recordId)
        {
            Record record = _unitOfWork.RecordRepository.Where(x => x.Id == recordId && x.Status == RecordState.InFinish).FirstOrDefault();
            if (record.Status.ToString() == RecordState.InAlram)
            {
                return true;
            }

            return false;
        }

        private async Task updateRecordToEnd(Guid recordId, int levelAction = 0)
        {

            Record record = await _unitOfWork.RecordRepository.GetById(recordId);
            if (record == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");
            }
            if (levelAction > 0)
            {
                //? 
                // await Task.Delay(TimeSpan.FromMinutes(3));
                if (_unitOfWork.RecordRepository.Where(x => x.Id == recordId && x.Status == RecordState.InFinish).FirstOrDefault() is null)
                {
                    await updateRecord(record);
                }
            }
            else
            {
                await updateRecord(record);
            }

        }

        private async Task updateRecord(Record record)
        {
            record.Status = RecordState.InFinish;
            record.FinishAlarmTime = DateTime.UtcNow.AddHours(7);
            //record.UserRatingPercent = await _memoryCacheService.GetResult(record.Id,CacheType.VotingLevel);
            _unitOfWork.RecordRepository.Update(record);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<PagedResult<RecordResponse>> Get(PagingRequest pagingRequest, RecordRequest req)
        {
            if (pagingRequest.ColName == null)
            {
                pagingRequest.ColName = "Id"; //Init default Id
            }

            var entity = _mapper.Map<Record>(req);

            var query = await _unitOfWork.RecordRepository.GetAll();
            query = query
                .Include(noti => noti.NotificationLogs)
                .Include(recordProcess => recordProcess.RecordProcesses) /*TODO User Voting*/
                    .ThenInclude(action => action.ActionType)
                .Include(record => record.Camera)
                    .ThenInclude(camera => camera.Location);

            var records = await query.ToListAsync();

            query = query.Where(x => x.RecordTime.Date >= req.FromDate.Date && x.RecordTime.Date <= req.ToDate.Date);


            #region filter query
            if (req.CameraId != Guid.Empty)
            {
                query = query.Where(x => x.CameraID == req.CameraId);
            }

            if (req.LocationId != Guid.Empty)
            {
                query = query.Where(x => x.Camera.LocationID == req.LocationId);
            }

            if (req.Status != null)
            {
                query = query.Where(x => x.Status == req.Status);
            }
            #endregion
            var entityProjected = LinqUtils.DynamicFilter<Record>(query, entity).ProjectTo<RecordResponse>(_mapper.ConfigurationProvider);

            var test = await query.ToListAsync();

            var sort = PageHelper<RecordResponse>.Sorting(pagingRequest.SortType, entityProjected, pagingRequest.ColName);
            var pagedEntity = PageHelper<RecordResponse>.Paging(sort, pagingRequest.Page, pagingRequest.PageSize);

            return pagedEntity;
        }

        public async Task<RecordDetailResponse> GetDetail(Guid recordID)
        {
            var response = await _unitOfWork.RecordRepository.RecordDetailResponse(recordID);
            //? check this Id in system
            if (await _unitOfWork.RecordRepository.GetById(recordID) == null)
            {
                throw new Exception();
            }
            //if ( _unitOfWork.MediaRecordRepository.Where(x => x.RecordId == recordID && x.Url.Contains("evidence")).Count() != 0)
            //{
            //    response.evidences = await _unitOfWork.MediaRecordRepository.Where(x => x.RecordId == recordID && x.Url.Contains("evidence"))?.Select(x => x.Url)?.ToListAsync() ?? null;
            //}
            if (response.RecordType == 3)
            {
                var record = await _unitOfWork.RecordRepository.GetById(recordID);
                var user = await _unitOfWork.UserRepository.GetById(record.CreatedBy);
                UserInLocationResponse response1 = new UserInLocationResponse()
                {
                    Name = user.Name,
                    UserID = user.Id,

                };
                response.AlarmUser = response1;
            }
            return response;
        }

        public async Task AutoAction(Guid recordID, int actioTypeId)
        {

            throw new NotImplementedException();


        }

        public async Task EndVotePhase(Guid recordID)
        {
            Record record = await _unitOfWork.RecordRepository.GetById(recordID);
            if (record == null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");
            }
            else
            {
                record.Status = RecordState.EndVote;
                _unitOfWork.RecordRepository.Update(record);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<NotificationAlarmResponse>> GetNotificationAlarm()
        {
            var data = await _unitOfWork.RecordRepository.NotificationAlarmResponse(_claimService.GetCurrentUserId);
            return data;
        }
        public async Task NotificationFakeAlarm(string locationName)
        {
            List<Guid> users = await _locationScopeService.GetUserInLocation(locationName, 1);
            users.Add(Guid.Parse("3c9a2a1b-f4dc-4468-a89c-f6be8ca3b541"));
            NotficationDetailResponse data = await NotificationHandler.Get(8);
            //? notification one time
            foreach (var item in users)
            {
                Console.WriteLine(item);

                string token = await RealtimeDatabaseHandlers.GetFCMTokenByUserID(item);
                string tokenReduce = token.Replace("\"", "");
                await CloudMessagingHandlers.CloudMessaging(
                          data.Title,
                          data.Context, tokenReduce
                          );
            }
        }
        public async Task AddEvidence(IFormFile file, Guid RecordId)
        {
            var record = await _unitOfWork.RecordRepository.GetById(RecordId);
            if (record == null)
            {
                throw new Exception("Not in database ");

            }
            var mediaRecords = _unitOfWork.MediaRecordRepository.Where(x => x.RecordId == RecordId && x.MediaTypeId == 2).Count();
            if (mediaRecords == 1)
            {
                var modifyFile = new FormFile(file.OpenReadStream(), 0, file.Length, null, $"evidene1_{RecordId}.png")
                {
                    Headers = file.Headers,
                    ContentType = file.ContentType
                };
                await StorageHandlers.UploadFileAsync(modifyFile, "ImageEvidene");
                MediaRecord newMediaRecord = new MediaRecord { RecordId = RecordId, MediaTypeId = 2, Url = modifyFile.FileName };
                _unitOfWork.MediaRecordRepository.InsertAsync(newMediaRecord);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                var modifyFile = new FormFile(file.OpenReadStream(), 0, file.Length, null, $"evidene{mediaRecords}_{RecordId}.png")
                {
                    Headers = file.Headers,
                    ContentType = file.ContentType
                };
                await StorageHandlers.UploadFileAsync(modifyFile, "ImageEvidene");
                MediaRecord newMediaRecord = new MediaRecord { RecordId = RecordId, MediaTypeId = 2, Url = modifyFile.FileName };
                _unitOfWork.MediaRecordRepository.InsertAsync(newMediaRecord);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<IEnumerable<NotificationAlarmResponse>> GetDisconnectedNotificationAlarm()
        {
            var data = await _unitOfWork.RecordRepository.NotificationDisconnected();
            return data;
        }

    }
}
