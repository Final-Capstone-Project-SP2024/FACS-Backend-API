﻿using FireDetection.Backend.Domain.DTOs.Request;
using FireDetection.Backend.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IRecordService
    {
        Task<bool> VoteAlarmLevel(Guid recordID ,RateAlarmRequest request);

        Task<bool> ActionInAlarm(Guid recordID,AddRecordActionRequest request);

        Task<IEnumerable<RecordResponse>> Get();
        
        Task<RecordDetailResponse> GetDetail(Guid recordID);
    }
}
