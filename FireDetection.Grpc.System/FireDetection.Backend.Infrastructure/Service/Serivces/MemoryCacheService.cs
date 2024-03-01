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
    public class MemoryCacheService :   IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        static int Rating = 0;
        static readonly object lockObject = new object();
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

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


        public async Task<bool> CheckRecordKeyIsVote(Guid recoid)
        {

            _memoryCache.TryGetValue(recoid.ToString(), out var result);

            if (result.ToString() == "Not")
            {
                return false;

            }
            return true;
        }

        public async Task CreateRecordKey(Guid recordID)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                   .SetPriority(CacheItemPriority.Normal)
                   .SetSize(1024);
            _memoryCache.Set($"{recordID}", "Not", cacheEntryOptions);


        }

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

        public async Task SetLevelAction(Guid recordId, int Level)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromMinutes(5))
           .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
           .SetPriority(CacheItemPriority.Normal)
           .SetSize(1024);
            _memoryCache.Set($"{recordId}_Level", Level, cacheEntryOptions);
        }

        public async Task UncheckRecordKey(Guid recordId)
        {
            _memoryCache.Set($"{recordId}", "Yes" );
        }


        public  async Task  CountingVote(int value)
        {
            // Update the global variable with proper synchronization
            await Task.Run(() => Increase(value, out Rating));
        }

        public void Increase(int value ,out int input)
        {
            input =+ value;
            
        }


        public async Task CancelAutoAction(Guid recordId)
        {
            _memoryCache.Set($"{recordId}_Action", "Yes");
        }

        public async Task<bool> CheckIsAction(Guid recordId)
        {
            _memoryCache.TryGetValue($"{recordId}_Action", out var result);

            if (result.ToString() == "Not")
            {
                return false;

            }
            return true;
        }

        public async Task CreateCheckAction(Guid recordId)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
          .SetSlidingExpiration(TimeSpan.FromMinutes(5))
          .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
          .SetPriority(CacheItemPriority.Normal)
          .SetSize(1024);
            _memoryCache.Set($"{recordId}_Action", "Not", cacheEntryOptions);
       
        }

        public async Task<int> VotingResult()
        {
            return Rating;
        }
    }
}
