using FireDetection.Backend.Domain.DTOs.Request;
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

        public Task AutoAction(Guid recordID, int actioTypeId);

        Task<PagedResult<RecordResponse>> Get(PagingRequest pagingRequest, RecordRequest req);
        
        Task<RecordDetailResponse> GetDetail(Guid recordID);

        Task EndVotePhase(Guid recordID);

        public Task<IEnumerable<NotificationAlarmResponse>> GetNotificationAlarm();


    }
}
