using FireDetection.Backend.Domain.DTOs.State;
using FireDetection.Backend.Domain.Entity;
using FireDetection.Backend.Infrastructure.Service.IServices;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FireDetection.Backend.Infrastructure.Service.Serivces
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }



        //todo: Add New MemoryCacheData
        /*
        1. Is Voting ( Guid_Voting )
        2. Is Action (Guid_Action)
        3. Save Counting to Voting get the biggest one ( Guid_Voting_Level )
        4. Save value to log notification ( RecordId_Action_Counting )
        */
        public async Task Create(Guid recordId, CacheType cacheType, int? VotingInput = 0)
        {
            string Name = NameTransfer(cacheType, recordId);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSlidingExpiration(TimeSpan.FromHours(1))
                 .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                 .SetPriority(CacheItemPriority.Normal)
                 .SetSize(1024);


            if (Name.Contains(CacheType.VotingLevel.GetDisplayName())) _memoryCache.Set(Name, VotingInput, cacheEntryOptions);

            if (Name.Contains(CacheType.IsFinish.GetDisplayName()) || Name.Contains(CacheType.Voting.GetDisplayName()) || Name.Contains(CacheType.Action.GetDisplayName()) || Name.Contains(CacheType.IsVoting.GetDisplayName())) _memoryCache.Set(Name, "Not", cacheEntryOptions);

            if (Name.Contains("_Counting")) _memoryCache.Set(Name, VotingInput, cacheEntryOptions);

            Console.WriteLine(Name);

        }


        public async Task UnCheck(Guid recordId, CacheType cacheType)
        {
            string Name = NameTransfer(cacheType, recordId);
            if (Name.Contains(CacheType.Action.GetDisplayName()) || Name.Contains(CacheType.IsVoting.GetDisplayName())) _memoryCache.Set(Name, "Yes");
        }

        private static string NameTransfer(CacheType? name, Guid recordId) => name switch
        {
            CacheType.Action or CacheType.VotingLevel or CacheType.IsVoting or CacheType.IsFinish => $"{recordId}_{name}",
            CacheType.AlarmLevel1 or CacheType.AlarmLevel2 or CacheType.AlarmLevel4 or CacheType.Voting
            or CacheType.AlarmLevel5 or CacheType.FireNotify or CacheType.FakeAlarm or CacheType.DisconnectCamera or CacheType.VotingValue
            or CacheType.VotingCount => $"{recordId}_{name}_Counting",

        };




        public async Task<bool> checkDisableVote(Guid recordId)
        {
            _memoryCache.TryGetValue("Disable", out var result);
            if (result is null)
            {
                Task.Run(async () => await SetDisableVote(recordId));
                return false;
            }
            if (result.ToString() == recordId.ToString())
            {
                return true;
            }
            return false;
        }


        public async Task<bool> CheckRecordKeyIsVote(Guid recordID)
        {

            _memoryCache.TryGetValue(NameTransfer(CacheType.IsVoting, recordID), out var result);

            if (result.ToString() == "Not")
            {
                return false;

            }
            return true;
        }

        //Task: out 
        //Task: out 
        public async Task SetDisableVote(Guid recordId)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                 .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                 .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                 .SetPriority(CacheItemPriority.Normal)
                 .SetSize(1024);
            _memoryCache.Set("Disable", recordId, cacheEntryOptions);

            await Task.Delay(20000);

        }

        public async Task UncheckRecordKey(Guid recordId)
        {
            _memoryCache.Set($"{recordId}", "Yes");
        }

        public async Task<bool> CheckIsAction(Guid recordId)
        {
            _memoryCache.TryGetValue(NameTransfer(CacheType.Action, recordId), out var result);

            if (result.ToString() == "Not")
            {
                return false;

            }
            return true;
        }
        public async Task SettingCount(Guid recordid, CacheType type, int count)
        {
            if (type == CacheType.VotingValue)
            {
                int value = (int)_memoryCache.Get(NameTransfer(type, recordid));
                if (count > value)
                {
                    _memoryCache.Set(NameTransfer(type, recordid), count);
                }
            }
            else
            {
                _memoryCache.Set(NameTransfer(type, recordid), count);

            }

        }

        public Task CheckVotingValue(Guid recordId, CacheType type, int? value)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckIsFinish(Guid recordId)
        {
            _memoryCache.TryGetValue(NameTransfer(CacheType.IsFinish, recordId), out var result);

            if (result.ToString() == "Not")
            {
                return false;

            }
            return true;
        }

        public Task<int> VotingResult()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetResult(Guid recordId, CacheType type)
        {
            int result = 0;
            if (type == CacheType.VotingValue) { result = (int)_memoryCache.Get(NameTransfer(CacheType.VotingValue, recordId)); }
            if (type == CacheType.FireNotify) { result = (int)_memoryCache.Get(NameTransfer(CacheType.FireNotify, recordId)); }
            if(type == CacheType.VotingCount) { result = (int)_memoryCache.Get(NameTransfer(CacheType.VotingCount, recordId)); }
            if (type == CacheType.AlarmLevel1) { result = (int)_memoryCache.Get(NameTransfer(CacheType.AlarmLevel1, recordId)); }
            if (type == CacheType.AlarmLevel2) { result = (int)_memoryCache.Get(NameTransfer(CacheType.AlarmLevel2, recordId)); }
            if (type == CacheType.AlarmLevel3) { result = (int)_memoryCache.Get(NameTransfer(CacheType.AlarmLevel3, recordId)); }
            if (type == CacheType.AlarmLevel4) { result = (int)_memoryCache.Get(NameTransfer(CacheType.AlarmLevel4, recordId)); ; }
            if (type == CacheType.AlarmLevel5) { result = (int)_memoryCache.Get(NameTransfer(CacheType.AlarmLevel5, recordId)); }
            Console.WriteLine(result + "Get result ");
            return result;
        }


        /// <summary>
        ///  increase quantity 
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="cacheType"></param>
        /// <returns></returns>
        public async Task IncreaseQuantity(Guid recordId, CacheType cacheType)
        {
            _memoryCache.TryGetValue(NameTransfer(cacheType, recordId), out int result);
            result++;
            _memoryCache.Set(NameTransfer(cacheType, recordId), result);

            Console.WriteLine(result);
        }

        public async Task<int> CheckEnoughVoting(Guid recordId)
        {
            _memoryCache.TryGetValue(NameTransfer(CacheType.VotingCount, recordId), out int result);
            return (int)result;
        }
    }
}
