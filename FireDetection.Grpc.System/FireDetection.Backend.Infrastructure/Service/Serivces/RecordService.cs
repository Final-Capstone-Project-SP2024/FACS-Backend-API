using AutoMapper;
using AutoMapper.QueryableExtensions;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Domain.Helpers.GetHandler;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Helpers.GetHandler;
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
    public class RecordService : IRecordService
    {
        private readonly ITimerService _timerService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly INotificationLogService _log;
        private readonly IClaimsService _claimService;

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCacheService memoryCacheService, ITimerService timerService, INotificationLogService log, IClaimsService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCacheService = memoryCacheService;
            _timerService = timerService;
            _log = log;
            _claimService = claimService;   
        }
        public async Task<bool> ActionInAlarm(Guid recordID, AddRecordActionRequest request)
        {

            //? check input can action type 
            if (!await CheckActionInSystem(recordID, request.ActionId)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Can not action smaller than the action before");

            // if (!await checkActionInRecord(recordID)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");

            if (request.ActionId < 6)
            {
                //todo save data int variale sutiable with request.actionID
                await _memoryCacheService.Create(recordID, setKey(request.ActionId));
                await _memoryCacheService.Create(recordID, CacheType.Voting);
                await _memoryCacheService.Create(recordID, CacheType.IsFinish);
                _timerService.SpamNotification(recordID, request.ActionId);
            }


            //? cancel check
            await _memoryCacheService.UnCheck(recordID, CacheType.Action);
            if (request.ActionId == 6)
            {
                List<RecordProcess> idInput = _unitOfWork.RecordProcessRepository.Where(x => x.RecordID == recordID).ToList();
                List<int> output = new List<int>();
                foreach (var item in idInput)
                {
                    output.Add(item.ActionTypeId);
                }
                Console.WriteLine(await _memoryCacheService.GetResult(recordID, CacheType.VotingValue));
                _log.SaveNotificationVotingRequire(recordID, await _memoryCacheService.GetResult(recordID, CacheType.VotingValue));
                foreach (var item in output)
                {
                    _log.SaveNotificationActionRequire(recordID, await _memoryCacheService.GetResult(recordID, setKey(item)), setKey(item));
                    Console.WriteLine(await _memoryCacheService.GetResult(recordID, setKey(item)));
                }
                await _unitOfWork.SaveChangeAsync();
                await updateRecordToEnd(recordID);
            }
            if (request.ActionId == 7) await updateRecordToEnd(recordID);

            RecordProcess recordProcess = _mapper.Map<RecordProcess>(request);
            recordProcess.RecordID = recordID;
            _unitOfWork.RecordProcessRepository.InsertAsync(recordProcess);
            await _unitOfWork.SaveChangeAsync();


            // updateRecordToEnd(recordID, 1);

            return true;
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
            var userId =  _claimService.GetCurrentUserId;
            if(_unitOfWork.AlarmRateRepository.Where(x => x.RecordID == recordID && x.UserID == userId) is not null)
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "You have voted before");
            }
            //Create Voting Point for RecordId 
            if (_unitOfWork.AlarmRepository.Where(x => x.RecordID == recordID).FirstOrDefault() is null)
            {

                //? to get the biggest value : create
                await _memoryCacheService.Create(recordID, CacheType.VotingValue, request.LevelRating);

                //? to count how many vote in this record 
                await _memoryCacheService.Create(recordID, CacheType.VotingCount, 1);


            }
            else
            {
                //? to get the biggest value : set 
                await _memoryCacheService.SettingCount(recordID, CacheType.VotingValue, request.LevelRating);

                //? to count how many vote in this record 
                await _memoryCacheService.IncreaseQuantity(recordID, CacheType.VotingCount);
            }

            //todo after 5 vote or 2 minutes it will auto user can not voting for this userId
            //? set voteCount
            //? start 2 minutes to end 
            _timerService.EndVotePhase(recordID);


            //todo check and create action checking 
            await _memoryCacheService.UnCheck(recordID, CacheType.IsVoting);
            await _memoryCacheService.Create(recordID, CacheType.Action);
            _timerService.CheckIsAction(recordID);


            Console.WriteLine(await _memoryCacheService.GetResult(recordID, CacheType.FireNotify));
            //todo save log the fireNotify Task
            await _log.SaveNotificationFireNotifyLog(recordID, await _memoryCacheService.GetResult(recordID, CacheType.FireNotify));
            request.LevelRating = request.LevelRating == 0 ? 6 : request.LevelRating;
            AlarmRate alarmRate = _mapper.Map<AlarmRate>(request);
            alarmRate.RecordID = recordID;
            alarmRate.UserID = userId;
            _unitOfWork.AlarmRateRepository.InsertAsync(alarmRate);
            return await _unitOfWork.SaveChangeAsync() > 0;

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
                .Include(alarm => alarm.AlarmRates) //TODO user rating
                .Include(noti => noti.NotificationLogs)
                .Include(recordProcess => recordProcess.RecordProcesses) /*TODO User Voting*/
                    .ThenInclude(action => action.ActionType)
                .Include(record => record.Camera)
                    .ThenInclude(camera => camera.Location);

            var records = await query.ToListAsync();

            query = query.Where(x => x.RecordTime.Date >= req.FromDate.Date && x.RecordTime.Date <= req.ToDate.Date);

            var entityProjected = LinqUtils.DynamicFilter<Record>(query, entity).ProjectTo<RecordResponse>(_mapper.ConfigurationProvider);

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

            var test = await query.ToListAsync();

            var sort = PageHelper<RecordResponse>.Sorting(pagingRequest.SortType, entityProjected, pagingRequest.ColName);
            var pagedEntity = PageHelper<RecordResponse>.Paging(sort, pagingRequest.Page, pagingRequest.PageSize);

            return pagedEntity;
        }

        public async Task<RecordDetailResponse> GetDetail(Guid recordID)
        {
            return _unitOfWork.RecordRepository.RecordDetailResponse(recordID);
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
            var data =  await _unitOfWork.RecordRepository.NotificationAlarmResponse();
            return data;
        }


        //  private async Task SaveVoteAndAction(Guid recordId, Type )
    }
}
