using AutoMapper;
using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> ActionInAlarm(Guid recordID, AddRecordActionRequest request)
        {
            RecordProcess recordProcess = _mapper.Map<RecordProcess>(request);
            recordProcess.RecordID = recordID;
            _unitOfWork.RecordProcessRepository.InsertAsync(recordProcess);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> VoteAlarmLevel(Guid recordID, RateAlarmRequest request)
        {
            AlarmRate alarmRate = _mapper.Map<AlarmRate>(request);
            alarmRate.RecordID = recordID;
            _unitOfWork.AlarmRateRepository.InsertAsync(alarmRate);
            return await _unitOfWork.SaveChangeAsync() > 0;

        }


    
    }
}
