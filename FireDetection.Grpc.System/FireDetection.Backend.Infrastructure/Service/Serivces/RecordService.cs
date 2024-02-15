using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using FireDetection.Backend.Infrastructure.Service.DAL;
using FireDetection.Backend.Infrastructure.Service.IServices;
using FireDetection.Backend.Infrastructure.UnitOfWork;
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

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCacheService memoryCacheService, ITimerService timerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _memoryCacheService = memoryCacheService;
            _timerService = timerService;

        }

        public RecordService()
        {
            
        }

        public async Task<bool> ActionInAlarm(Guid recordID, AddRecordActionRequest request)
        {

            if (!await CheckActionInSystem(recordID, request.ActionId)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");

            if (!await checkActionInRecord(recordID)) throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");



            await _memoryCacheService.CancelAutoAction(recordID);
            if (request.ActionId == 5 || request.ActionId == 6) await updateRecordToEnd(recordID);

            RecordProcess recordProcess = _mapper.Map<RecordProcess>(request);
            recordProcess.RecordID = recordID;
            _unitOfWork.RecordProcessRepository.InsertAsync(recordProcess);
            await _unitOfWork.SaveChangeAsync();
            Task.Run(async () => await updateRecordToEnd(recordID, 1));

            return true;
        }

        public async Task<bool> VoteAlarmLevel(Guid recordID, RateAlarmRequest request)
        {

            //  await _memoryCacheService.SetRecordKey(recordID);
            if (await _memoryCacheService.checkDisableVote(recordID))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest, "Have already this location in system");
            };
            await _memoryCacheService.UncheckRecordKey(recordID);

            await _memoryCacheService.CreateCheckAction(recordID);
            _timerService.CheckIsAction(recordID);
            AlarmRate alarmRate = _mapper.Map<AlarmRate>(request);
            alarmRate.RecordID = recordID;
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
            Record record = await _unitOfWork.RecordRepository.GetById(recordId);
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
                await Task.Delay(TimeSpan.FromMinutes(3));
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

        public async Task<IEnumerable<RecordResponse>> Get()
        {
            return _unitOfWork.RecordRepository.Get();
        }

        public async Task<RecordDetailResponse> GetDetail(Guid recordID)
        {
            return _unitOfWork.RecordRepository.RecordDetailResponse(recordID);
        }

        public async Task AutoAction(Guid recordID, int actioTypeId)
        {

            throw new NotImplementedException();


        }
    }
}
