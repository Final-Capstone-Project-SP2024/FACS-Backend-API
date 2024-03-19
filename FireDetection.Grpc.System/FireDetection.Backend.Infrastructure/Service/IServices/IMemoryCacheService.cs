using FireDetection.Backend.Domain.DTOs.State;
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
        public  Task SettingCount(Guid recordid, CacheType type, int  count);
        public Task<bool> checkDisableVote(Guid recordId);
        public Task SetDisableVote(Guid recordId);
        public Task UnCheck(Guid recordId, CacheType cacheType);
        public Task<bool> CheckIsAction(Guid recordId);
        public Task<bool> CheckIsFinish(Guid recordId);
        public Task<int> VotingResult();
        public Task Create(Guid recordId, CacheType cacheType, int? VotingInput = 0);
        public Task CheckVotingValue(Guid recordId,CacheType type,int? value);

        public Task<int> GetResult(Guid recordId, CacheType type);
        public Task IncreaseQuantity(Guid recordId, CacheType cacheType);

        public Task<int> CheckEnoughVoting(Guid recordId);

        public  Task SaveOTP(int otp, string mail);

        public Task<int> GetOTP(string email);
    }
}
