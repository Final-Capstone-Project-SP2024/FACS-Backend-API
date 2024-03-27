using FireDetection.Backend.Domain.DTOs.Response;
using FireDetection.Backend.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Repository.IRepositories
{
    public interface IRecordRepository : IGenericRepository<Record>
    {
        public IEnumerable<RecordResponse> Get();

        public Task<RecordDetailResponse> RecordDetailResponse(Guid recordId);
        public Task<IEnumerable<NotificationAlarmResponse>> NotificationAlarmResponse();
    }
}
