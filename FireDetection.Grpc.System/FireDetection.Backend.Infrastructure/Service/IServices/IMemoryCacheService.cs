using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Infrastructure.Service.IServices
{
    public interface IMemoryCacheService
    {
        public Task<bool> CheckRecordKeyIsVote(Guid recoid);

        public Task  CreateRecordKey(Guid recordid); 


        public Task<bool> checkDisableVote(Guid recordId);

        public Task SetDisableVote(Guid recordId);

        public Task UncheckRecordKey(Guid recordId);

        public void CountingVote(int value, out int input);

        public Task<bool> CheckIsAction(Guid recordId);

        public Task CancelAutoAction(Guid recordId);

        public Task CreateCheckAction(Guid recordId);

        public Task<int> VotingResult();
    }
}
